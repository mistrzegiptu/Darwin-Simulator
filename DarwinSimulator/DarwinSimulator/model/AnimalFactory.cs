using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal class AnimalFactory
    {
        public static Animal createAnimal(Vector2d startingPosition, Parameters parameters)
        {
            switch (parameters.AnimalParameters.AnimalType)
            {
                case AnimalType.NORMAL_ANIMAL: 
                    return new NormalAnimal(startingPosition, parameters);
                default:
                    throw new ArgumentException();
            }
        }

        public static Animal createAnimal(Genome genome, Vector2d position, int energy, Parameters parameters)
        {
            switch (parameters.AnimalParameters.AnimalType)
            {
                case AnimalType.NORMAL_ANIMAL:
                    return new NormalAnimal(genome, position, energy, parameters);
                default:
                    throw new ArgumentException();
            }
        }
    }
}
