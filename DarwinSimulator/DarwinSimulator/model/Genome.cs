using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal abstract class Genome
    {
        protected readonly Parameters parameters;
        protected readonly Random rand = new Random();
        protected int[] genes;
        protected readonly int genomeLength;
        protected readonly int geneMaxVal;
        protected int currentIndex;

        public Genome(Parameters parameters)
        {
            this.parameters = parameters;
            genomeLength = parameters.GenomeParameters.GenomeLength;
            genes = new int[genomeLength];
            currentIndex = rand.Next(0, genomeLength);
            geneMaxVal = MapDirectionExtension.GetLength();

            genes.Select(x => x = rand.Next(geneMaxVal + 1));
        }

        public Genome(Genome left, Genome right, int leftEnergy, int rightEnergy, Parameters parameters)
        {
            this.parameters = parameters;
            genomeLength = parameters.GenomeParameters.GenomeLength;
            genes = new int[genomeLength];
            currentIndex = rand.Next(genomeLength);
            geneMaxVal = MapDirectionExtension.GetLength();

            int leftLength = genomeLength * leftEnergy / (leftEnergy + rightEnergy);
            int rightLength = genomeLength - leftLength;

            if (rand.Next(2) == 0)
                genes = left.genes.Take(leftLength).Concat(right.genes.Skip(leftLength)).ToArray();
            else
                genes = right.genes.Take(rightLength).Concat(left.genes.Skip(rightEnergy)).ToArray();

            Mutate();
        }

        public int GetNext()
        {
            currentIndex = (currentIndex + 1) % genomeLength;
            return genes[currentIndex];
        }

        public void JumpToRandom()
        {
            currentIndex = rand.Next(genomeLength);
        }

        protected abstract void Mutate();
    }
}
