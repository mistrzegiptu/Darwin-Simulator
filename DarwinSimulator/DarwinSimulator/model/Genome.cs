using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal abstract class Genome
    {
        public Parameters Parameters { get; }
        protected readonly Random rand = new Random();
        protected int[] genes;
        protected readonly int genomeLength;
        protected int currentIndex;

        public Genome(Parameters parameters)
        {
            Parameters = parameters;
            genomeLength = parameters.GenomeParameters.GenomeLength;
            genes = new int[genomeLength];
            currentIndex = rand.Next(0, genomeLength);

            genes.Select(x => x = rand.Next(MapDirectionExtension.GetLength() + 1));
        }

        public int GetNext()
        {
            currentIndex = (currentIndex + 1) % genomeLength;
            return genes[currentIndex];
        }

        protected abstract void Mutate();
    }
}
