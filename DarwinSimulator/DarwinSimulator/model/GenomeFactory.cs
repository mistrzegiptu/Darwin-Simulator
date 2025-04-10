﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public class GenomeFactory
    {
        public static Genome CreateGenome(Parameters parameters)
        {
            switch (parameters.GenomeParameters.GenomeType)
            {
                case GenomeType.NORMAL_GENOME:
                    return new NormalGenome(parameters);
                case GenomeType.SWAPPING_GENOME:
                    return new SwappingGenome(parameters);
                case GenomeType.SLIGHTLY_ADJUSTING_GENOME:
                    return new SlightlyAdjustingGenome(parameters);
                default:
                    throw new ArgumentException();
            }
        }

        public static Genome CreateGenome(Genome left, Genome right, int leftEnergy, int rightEnergy, Parameters parameters)
        {
            switch (parameters.GenomeParameters.GenomeType)
            {
                case GenomeType.NORMAL_GENOME:
                    return new NormalGenome(left, right, leftEnergy, rightEnergy, parameters);
                case GenomeType.SWAPPING_GENOME:
                    return new SwappingGenome(left, right, leftEnergy, rightEnergy, parameters);
                case GenomeType.SLIGHTLY_ADJUSTING_GENOME:
                    return new SlightlyAdjustingGenome(left, right, leftEnergy, rightEnergy, parameters);
                default:
                    throw new ArgumentException();
            }
        }
    }
}
