using DarwinSimulator.model.records;

namespace DarwinSimulator.model
{
    public abstract class WorldMap : IWorldMap, IMoveValidator
    {
        protected readonly Random rand = new Random();
        protected readonly Parameters parameters;
        public Boundary Boundary { get; protected set; }

        protected readonly IPlanter planter;

        protected readonly Dictionary<Vector2d, List<Animal>> animals = new();
        protected readonly Dictionary<Vector2d, IWorldElement> plants = new();

        public Dictionary<Vector2d, List<Animal>> Animals
        {
            get => animals;
        }
        public List<Animal> DeadAnimals
        {
            get => deadAnimals;
        }
        public WorldStats WorldStats
        {
            get => new WorldStats(_animalsCount, _plantsCount, GetFreeFieldsCount(), GetMostPopularGenome(), GetAverageEnergy(), GetAverageLifetime(), GetAverageChildCount());
        }

        private int _animalsCount = 0;
        private int _plantsCount = 0;

        protected readonly List<Animal> deadAnimals = new();

        public event Action<Vector2d>? AnimalDied;


        public WorldMap(Parameters parameters)
        {
            this.parameters = parameters;

            Vector2d lowerLeft = new Vector2d(0, 0);
            Vector2d upperRight = new Vector2d(parameters.WorldParameters.Width - 1, parameters.WorldParameters.Height - 1);
            Boundary = new Boundary(lowerLeft, upperRight);

            planter = PlanterFactory.CreatePlanter(parameters, this);

            SpawnNewPlants(parameters.WorldParameters.StartingPlantCount);
            SpawnAnimalsAtStart(parameters.WorldParameters.StartingAnimalCount);
        }

        public virtual void PassDay(int day)
        {
            RemoveDeadAnimals(day);
            MoveAnimals();
            EatPlants();
            ReproduceAnimals();
            SpawnNewPlants(parameters.WorldParameters.DailyPlantGrow);
        }

        public void RemoveDeadAnimals(int day)
        {
            foreach(var animalsOnField in animals.Values)
            {
                deadAnimals.AddRange(animalsOnField.Where(x => x.IsAlive == false));

                List<Animal> animalsToRemove = new();

                foreach (var animal in animalsOnField)
                {
                    if (!animal.IsAlive)
                    {
                        animal.SetDeathDay(day - 1);
                        animalsToRemove.Add(animal);
                        AnimalDied?.Invoke(animal.Position);
                    }
                }

                animalsToRemove.ForEach(RemoveAnimal);
            }
        }

        public virtual void MoveAnimals()
        {
            List<Animal> animalsToMove = new();

            foreach (var animalsOnField in animals.Values)
            {    
                animalsToMove.AddRange(animalsOnField);
            }
            animalsToMove.ForEach(x => { RemoveAnimal(x); x.Move(this); PlaceAnimal(x); });
        }

        public void EatPlants()
        {
            foreach(var animalsOnField in animals.Values)
            {
                if (plants.ContainsKey(animalsOnField.First().Position))
                {
                    Animal eatingAnimal = animalsOnField.OrderByDescending(x => x.Energy).ThenByDescending(x => x.Age).ThenByDescending(x => x.ChildCount).First();

                    IWorldElement plant = plants[eatingAnimal.Position];

                    if (plant is BigPlant bigPlant)
                    {
                        eatingAnimal.Equals(parameters.WorldParameters.EnergyForEating * bigPlant.EnergyMultiplier);

                        foreach (var position in bigPlant.CoveredPositions)
                            plants.Remove(position);
                    }
                    else
                    {
                        eatingAnimal.EatPlant(parameters.WorldParameters.EnergyForEating);
                        plants.Remove(eatingAnimal.Position);
                    }
                }
            }
        }

        public void ReproduceAnimals()
        {
            foreach (var animalsOnField in animals.Values)
            {
                animalsOnField.OrderByDescending(x => x.Energy).ThenByDescending(x => x.Age).ThenByDescending(x => x.ChildCount);
                for (int i = 0; i < animalsOnField.Count - 1; i += 2)
                {
                    Animal? child;
                    
                    if(animalsOnField[i].TryReproduce(animalsOnField[i + 1], out child))
                    {
                        PlaceAnimal(child!);
                        _animalsCount++;
                    }
                }
            }
        }

        public void SpawnNewPlants(int plantCount)
        {
            _plantsCount += planter.SpawnNewPlants(plants, plantCount);
        }

        private void SpawnAnimalsAtStart(int animalCount)
        {
            for(int i = 0; i < animalCount; i++) 
            {
                int randX = rand.Next(Boundary.LowerLeft.X, Boundary.UpperRight.X);
                int randY = rand.Next(Boundary.LowerLeft.Y, Boundary.UpperRight.Y);
                
                Vector2d randPosition = new Vector2d(randX, randY);

                PlaceAnimal(AnimalFactory.CreateAnimal(randPosition, parameters));
                _animalsCount++;
            }
        }

        private void PlaceAnimal(Animal animal)
        {
            Vector2d position = animal.Position;

            if (!animals.ContainsKey(position))
                animals.Add(position, new List<Animal> {animal});
            else
                animals[position].Add(animal);
        }

        private void RemoveAnimal(Animal animal)
        {
            Vector2d position = animal.Position;

            animals[position].Remove(animal);

            if (animals[position].Count == 0)
                animals.Remove(position);
        }

        public virtual bool CanMoveTo(Vector2d position, ICreature callerCreature)
        {
            return position.Y <= Boundary.UpperRight.Y && position.Y >= Boundary.LowerLeft.Y;
        }

        public virtual Vector2d ChangeOnBound(Vector2d position, ICreature callerCreature)
        {
            if (position.X == Boundary.LowerLeft.X - 1)
                return new Vector2d(Boundary.UpperRight.X, position.Y);
            else if(position.X == Boundary.UpperRight.X + 1)
                return new Vector2d(Boundary.LowerLeft.X, position.Y);

            return position;
        }

        public virtual IWorldElement? ObjectAt(Vector2d position)
        { 
            if(animals.ContainsKey(position))
                return animals[position].First();
            else if(plants.ContainsKey(position))
                return plants[position];
            return null;
        }

        protected virtual int GetFreeFieldsCount()
        {
            int xLen = Boundary.UpperRight.X - Boundary.LowerLeft.X;
            int yLen = Boundary.UpperRight.Y - Boundary.LowerLeft.Y;

            int livingAnimals = animals.Values.Select(x => x.Where(y => y.IsAlive)).Count();
            int plantsCount = plants.Count();

            return xLen * yLen - livingAnimals - plantsCount;
        }

        protected String GetMostPopularGenome()
        {
            Dictionary<String, int> genomes = new Dictionary<String, int>();

            List<Animal> allAnimals = animals.Values.SelectMany(x => x).ToList();
            allAnimals.AddRange(deadAnimals);

            foreach(var animal in allAnimals)
            {
                if (genomes.ContainsKey(animal.Genome.ToString()))
                    genomes[animal.Genome.ToString()] += 1;
                else
                    genomes.Add(animal.Genome.ToString(), 1);
            }

            return genomes.OrderByDescending(x => x.Value).FirstOrDefault().Key;
        }

        protected double GetAverageEnergy()
        {
            return animals.Values.SelectMany(x => x).Select(x => x.Energy).DefaultIfEmpty(0).Average();
        }

        protected double GetAverageLifetime()
        {
            return deadAnimals.Select(x => x.Age).DefaultIfEmpty(0).Average();
        }

        protected double GetAverageChildCount()
        {
            return animals.Values.SelectMany(x => x).Select(x => x.ChildCount).DefaultIfEmpty(0).Average();
        }

        public bool CanPlant(Vector2d position)
        {
            return ObjectAt(position) == null;
        }
    }
}