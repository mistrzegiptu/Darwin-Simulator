using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal class SwappingGenome : Genome
    {
        public SwappingGenome(Parameters parameters) : base(parameters)
        {
        }

        public SwappingGenome(Genome left, Genome right, int leftEnergy, int rightEnergy, Parameters parameters) : base(left, right, leftEnergy, rightEnergy, parameters)
        {
        }

        protected override void Mutate()
        {
            int minMutationCount = Parameters.GenomeParameters.MinMutationCount;
            int maxMutationCount = Parameters.GenomeParameters.MaxMutationCount;
            int mutationCount = rand.Next(minMutationCount, maxMutationCount + 1);

            for(int i = 0; i < mutationCount; i++)
            {
                int first = rand.Next(genomeLength);
                int second = rand.Next(genomeLength);

                int temp = genes[first];
                genes[first] = genes[second];
                genes[second] = temp;
            }
        }
    }
}
