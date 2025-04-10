using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public abstract class Animal : IWorldElement
    {
        protected readonly Parameters parameters;
        protected static readonly Random rand = new Random();

        public Genome Genome { get; }
        public Vector2d Position { get; protected set; }

        protected MapDirection direction;
        public int Energy { get; protected set; } = 0;

        public int Age { get; protected set; } = 0;
        protected int plantsEaten = 0;
        public bool IsAlive { get; protected set; } = true;
        protected int deathDay = -1;
        protected readonly List<Animal> children = new List<Animal>();
        public int ChildCount
        {
            get => children.Count;
        }

        public Animal(Vector2d startingPosition, Parameters parameters)
        {
            Genome = GenomeFactory.CreateGenome(parameters);
            Energy = parameters.AnimalParameters.StartingEnergyLevel;
            Position = startingPosition;

            this.parameters = parameters;
        }

        public Animal(Genome genome, Vector2d position, int energy, Parameters parameters)
        {
            this.Genome = genome;
            this.Position = position;
            this.Energy = energy;
            this.parameters = parameters;
        }

        public bool TryReproduce(Animal secondParent, out Animal child)
        {
            Animal firstParent = this;
            child = null;

            if (firstParent.Energy < parameters.AnimalParameters.MinEnergyForReproducing || secondParent.Energy < parameters.AnimalParameters.MinEnergyForReproducing)
                return false;

            Genome childGenome = GenomeFactory.CreateGenome(firstParent.Genome, secondParent.Genome, firstParent.Energy, secondParent.Energy, parameters);

            child = AnimalFactory.CreateAnimal(childGenome, Position, parameters.AnimalParameters.EnergyUsedForReproducing * 2, parameters);

            firstParent.LoseEnergy(parameters.AnimalParameters.EnergyUsedForReproducing);
            secondParent.LoseEnergy(parameters.AnimalParameters.EnergyUsedForReproducing);

            firstParent.children.Add(child);
            secondParent.children.Add(child);

            return true;
        }

        public virtual void Move(IMoveValidator moveValidator, int energyLoss = 1)
        {
            direction = direction.Rotate(Genome.GetNext());
            Vector2d unitVector = direction.ToUnitVector();

            if (moveValidator.CanMoveTo(Position.Add(unitVector)))
                Position = Position.Add(unitVector);
            else
            {
                direction = direction.Reverse();
                unitVector = direction.ToUnitVector();
                Position = Position.Add(unitVector);
            }

            Position = moveValidator.ChangeOnBound(Position);

            LoseEnergy(energyLoss);
            Age++;
        }

        public void EatPlant(int energyForEating)
        {
            Energy += energyForEating;
            plantsEaten++;
        }

        protected void LoseEnergy(int energyAmount)
        {
            Energy -= energyAmount;

            if (Energy <= 0)
                Die();
        }

        public void Die()
        {
            IsAlive = false;
        }

        public void SetDeathDay(int day)
        {
            deathDay = day;
        }

        public override string ToString()
        {
            return "A";
        }
    }
}