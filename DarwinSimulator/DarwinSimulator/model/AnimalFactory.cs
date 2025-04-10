﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public class AnimalFactory
    {
        public static Animal CreateAnimal(Vector2d startingPosition, Parameters parameters)
        {
            switch (parameters.AnimalParameters.AnimalType)
            {
                case AnimalType.NORMAL_ANIMAL: 
                    return new NormalAnimal(startingPosition, parameters);
                case AnimalType.CRAZY_ANIMAL:
                    return new CrazyAnimal(startingPosition, parameters);
                case AnimalType.AGEING_ANIMAL:
                    return new AgeingAnimal(startingPosition, parameters);
                default:
                    throw new ArgumentException();
            }
        }

        public static Animal CreateAnimal(Genome genome, Vector2d position, int energy, Parameters parameters)
        {
            switch (parameters.AnimalParameters.AnimalType)
            {
                case AnimalType.NORMAL_ANIMAL:
                    return new NormalAnimal(genome, position, energy, parameters);
                case AnimalType.CRAZY_ANIMAL:
                    return new CrazyAnimal(genome, position, energy, parameters);
                case AnimalType.AGEING_ANIMAL:
                    return new AgeingAnimal(genome, position, energy, parameters);
                default:
                    throw new ArgumentException();
            }
        }
    }
}
