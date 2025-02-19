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

        protected readonly List<Animal> deadAnimals = new();
        protected readonly HashSet<Vector2d> preferredFields = new();
        protected readonly HashSet<Vector2d> nonPreferredFields = new();

        public event Action<Vector2d>? AnimalDied;
        // TODO: ADD MAP CHANGE LISTENERS

        public WorldMap(Parameters parameters)
        {
            this.parameters = parameters;

            planter = PlanterFactory.createPlanter(parameters, this);

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
                if(plants.ContainsKey(animalsOnField.First().Position))
                {
                    Animal eatingAnimal = animalsOnField.OrderByDescending(x => x.Energy).ThenByDescending(x => x.Age).ThenByDescending(x => x.ChildCount).First();
                    eatingAnimal.EatPlant(parameters.WorldParameters.EnergyForEating);
                    plants.Remove(eatingAnimal.Position);
                }
            }
        }
        
        public void ReproduceAnimals()
        {
            throw new NotImplementedException();
        }

        public void SpawnNewPlants(int plantCount)
        {
            planter.SpawnNewPlants(plants, plantCount);
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
    }
}
