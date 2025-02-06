using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal class NormalAnimal : Animal
    {
        public NormalAnimal(Vector2d startingPosition, Parameters parameters) : base(startingPosition, parameters)
        {
        }

        public NormalAnimal(Genome genome, Vector2d position, int energy, Parameters parameters) : base(genome, position, energy, parameters)
        {
        }
    }
}
