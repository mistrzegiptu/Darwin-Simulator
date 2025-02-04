using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal class SlightlyAdjustingGenome : Genome
    {
        public SlightlyAdjustingGenome(Parameters parameters) : base(parameters)
        {
        }

        public SlightlyAdjustingGenome(Genome left, Genome right, int leftEnergy, int rightEnergy, Parameters parameters) : base(left, right, leftEnergy, rightEnergy, parameters)
        {
        }

        protected override void Mutate()
        {
            int minMutationCount = Parameters.GenomeParameters.MinMutationCount;
            int maxMutationCount = Parameters.GenomeParameters.MaxMutationCount;
            int mutationCount = rand.Next(minMutationCount, maxMutationCount + 1);

            for (int i = 0; i < mutationCount; i++)
            {
                int geneToAdjust = rand.Next(genomeLength);

                if (rand.Next(2) == 0)
                    genes[geneToAdjust] = (genes[geneToAdjust] + 1) % geneMaxVal;
                else
                    genes[geneToAdjust] = (genes[geneToAdjust] - 1 + geneMaxVal) % geneMaxVal;
            }
        }
    }
}
