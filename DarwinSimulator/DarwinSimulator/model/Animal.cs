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
        protected Vector2d position;
        protected MapDirection direction;
        protected int energy = 0;

        protected int age = 0;
        protected int plantsEaten = 0;
        protected bool isAlive = true;
        protected int deathDay = -1;
        protected readonly List<Animal> children = new List<Animal>();

        public Animal(Vector2d startingPosition, Parameters parameters)
        {
            genome = GenomeFactory.createGenome(parameters);
            energy = parameters.AnimalParameters.StartingEnergyLevel;
            position = startingPosition;

            this.parameters = parameters;
        }

        public Animal(Genome genome, Vector2d position, int energy, Parameters parameters)
        {
            this.genome = genome;
            this.position = position;
            this.energy = energy;
            this.parameters = parameters;
        }

        public Animal? Reproduce(Animal secondParent)
        {
            Animal firstParent = this;

            if (firstParent.energy < parameters.AnimalParameters.MinEnergyForReproducing || secondParent.energy < parameters.AnimalParameters.MinEnergyForReproducing)
                return null;

            Genome childGenome = GenomeFactory.createGenome(firstParent.genome, secondParent.genome, firstParent.energy, secondParent.energy, parameters);

            Animal child = AnimalFactory.createAnimal(childGenome, position, parameters.AnimalParameters.MinEnergyForReproducing * 2, parameters);

            firstParent.loseEnergy(parameters.AnimalParameters.EnergyUsedForReproducing);
            secondParent.loseEnergy(parameters.AnimalParameters.EnergyUsedForReproducing);

            firstParent.children.Add(child);
            secondParent.children.Add(child);

            return child;
        }

        public virtual void Move(MoveValidator moveValidator)
        {
            direction = direction.Rotate(genome.GetNext());
            Vector2d unitVector = direction.ToUnitVector();

            if (moveValidator.CanMoveTo(position.Add(unitVector)))
                position.Add(unitVector);
            else
                direction = direction.Reverse();

            position = moveValidator.ChangeOnBound(position);

            loseEnergy(1);
            age++;
        }

        protected void loseEnergy(int energyAmount)
        {
            energy -= energyAmount;

            if (energy < 0)
                Die();
        }

        public void Die()
        {
            isAlive = false;
        }
    }
}
