using DarwinSimulator.model.records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal abstract class WorldMap : IWorldMap, IMoveValidator
    {
        protected readonly Random rand = new Random();
        protected readonly Parameters parameters;
        public Boundary Boundary { get; protected set; }

        protected readonly IPlanter planter;

        protected readonly Dictionary<Vector2d, List<Animal>> animals = new();
        protected readonly Dictionary<Vector2d, IWorldElement> plants = new();

        public WorldStats WorldStats
        {
            get => new WorldStats(_animalsCount, _plantsCount, GetFreeFieldsCount(), GetMostPopulatGenome(), GetAverageEnergy(), GetAveragaLifetime(), GetAverageChildCount());
        }

        private int _animalsCount = 0;
        private int _plantsCount = 0;

        protected readonly List<Animal> deadAnimals = new();

        public event Action<Vector2d>? AnimalDied;


        public WorldMap(Parameters parameters)
        {
            this.parameters = parameters;

            planter = PlanterFactory.CreatePlanter(parameters, this);

            Vector2d lowerLeft = new Vector2d(0, 0);
            Vector2d upperRight = new Vector2d(parameters.WorldParameters.Width, parameters.WorldParameters.Height);
            Boundary = new Boundary(lowerLeft, upperRight);
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

                foreach (var animal in animalsOnField)
                {
                    if(!animal.IsAlive)
                    {
                        animal.SetDeathDay(day-1);
                        RemoveAnimal(animal);
                        AnimalDied?.Invoke(animal.Position);
                    }
                }
            }
        }

        public virtual void MoveAnimals()
        {
            foreach (var animalsOnField in animals.Values)
            {
                foreach (var animal in animalsOnField)
                {
                    animal.Move(this);
                }
            }
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
                for (int i = 0; i < animalsOnField.Count; i += 2)
                {
                    Animal? child;
                    
                    if(animalsOnField[i].TryReproduce(animalsOnField[i + 1], out child))
                    {
                        PlaceAnimal(child!);
                    }
                }
            }
        }

        public void SpawnNewPlants(int plantCount)
        {
            planter.SpawnNewPlants(plants, plantCount);
            _plantsCount += plantCount;
        }

        private void PlaceAnimal(Animal animal)
        {
            Vector2d position = animal.Position;

            if (!animals.ContainsKey(position))
                animals.Add(position, new List<Animal> {animal});
            else
                animals[position].Add(animal);

            _animalsCount++;
        }

        private void RemoveAnimal(Animal animal)
        {
            Vector2d position = animal.Position;

            animals[position].Remove(animal);

            if (animals[position].Count == 0)
                animals.Remove(position);
        }

        public bool CanMoveTo(Vector2d position)
        {
            return position.Y <= Boundary.UpperRight.Y && position.Y >= Boundary.LowerLeft.Y;
        }

        public Vector2d ChangeOnBound(Vector2d position)
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

        protected virtual String GetMostPopulatGenome()
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

            return genomes.OrderByDescending(x => x.Value).First().Key;
        }

        protected virtual double GetAverageEnergy()
        {
            return animals.Values.SelectMany(x => x).Average(x => x.Energy);
        }

        protected virtual double GetAveragaLifetime()
        {
            return deadAnimals.Average(x => x.Age);
        }

        protected virtual double GetAverageChildCount()
        {
            return animals.Values.SelectMany(x => x).Average(x => x.ChildCount);
        }
    }
}