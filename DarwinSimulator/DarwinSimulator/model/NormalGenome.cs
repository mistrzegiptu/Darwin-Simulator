using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal class NormalGenome : Genome
    {
        public NormalGenome(Parameters parameters) : base(parameters)
        { 
        }
        
        public NormalGenome(Genome left, Genome right, int leftEnergy, int rightEnergy, Parameters parameters) : base(left, right, leftEnergy, rightEnergy, parameters)
        {
        }

        protected override void Mutate()
        {
            int minMutationCount = parameters.GenomeParameters.MinMutationCount;
            int maxMutationCount = parameters.GenomeParameters.MaxMutationCount;
            int mutationCount = rand.Next(minMutationCount, maxMutationCount + 1);

            for (int i = 0; i < mutationCount; i++)
            {
                int geneToSwap = rand.Next(genes.Length);
                int newGene = rand.Next(geneMaxVal + 1);

                genes[geneToSwap] = newGene;
            }
        }
    }
}
