using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal abstract class Animal
    {
        protected readonly Parameters parameters;
        protected static readonly Random rand = new Random();

        protected readonly Genome genome;
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
            genome = GenomeFactory.CreateGenome(parameters);
            Energy = parameters.AnimalParameters.StartingEnergyLevel;
            Position = startingPosition;

            this.parameters = parameters;
        }

        public Animal(Genome genome, Vector2d position, int energy, Parameters parameters)
        {
            this.genome = genome;
            this.Position = position;
            this.Energy = energy;
            this.parameters = parameters;
        }

        public bool TryReproduce(Animal secondParent, out Animal? child)
        {
            Animal firstParent = this;
            child = null;

            if (firstParent.Energy < parameters.AnimalParameters.MinEnergyForReproducing || secondParent.Energy < parameters.AnimalParameters.MinEnergyForReproducing)
                return false;

            Genome childGenome = GenomeFactory.CreateGenome(firstParent.genome, secondParent.genome, firstParent.Energy, secondParent.Energy, parameters);

            child = AnimalFactory.CreateAnimal(childGenome, Position, parameters.AnimalParameters.MinEnergyForReproducing * 2, parameters);

            firstParent.loseEnergy(parameters.AnimalParameters.EnergyUsedForReproducing);
            secondParent.loseEnergy(parameters.AnimalParameters.EnergyUsedForReproducing);

            firstParent.children.Add(child);
            secondParent.children.Add(child);

            return true;
        }

        public virtual void Move(IMoveValidator moveValidator, int energyLoss = 1)
        {
            direction = direction.Rotate(genome.GetNext());
            Vector2d unitVector = direction.ToUnitVector();

            if (moveValidator.CanMoveTo(Position.Add(unitVector)))
                Position.Add(unitVector);
            else
                direction = direction.Reverse();

            Position = moveValidator.ChangeOnBound(Position);

            loseEnergy(energyLoss);
            Age++;
        }

        public void EatPlant(int energyForEating)
        {
            Energy += energyForEating;
            plantsEaten++;
        }

        protected void loseEnergy(int energyAmount)
        {
            Energy -= energyAmount;

            if (Energy < 0)
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
    }
}