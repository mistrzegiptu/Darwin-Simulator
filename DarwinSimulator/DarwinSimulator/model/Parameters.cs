using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DarwinSimulator.model.records;

namespace DarwinSimulator.model
{
    internal class Parameters
    {
        public AnimalParameters AnimalParameters { get; }
        public GenomeParameters GenomeParameters { get; }
        public WorldParameters WorldParameters { get; }

        public Parameters(AnimalParameters animalParameters, GenomeParameters genomeParameters, WorldParameters worldParameters)
        {
            AnimalParameters = animalParameters;
            GenomeParameters = genomeParameters;
            WorldParameters = worldParameters;
        }
    }
}
