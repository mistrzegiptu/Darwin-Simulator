using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal class CrazyAnimal : Animal
    {
        public CrazyAnimal(Vector2d startingPosition, Parameters parameters) : base(startingPosition, parameters)
        {
        }

        public CrazyAnimal(Genome genome, Vector2d position, int energy, Parameters parameters) : base(genome, position, energy, parameters)
        {
        }

        public override void Move(MoveValidator moveValidator)
        {
            base.Move(moveValidator);

            int chanceToAnotherMove = rand.Next(100);

            if (chanceToAnotherMove < 20)
                genome.JumpToRandom();
            else
                base.Move(moveValidator);  
        }
    }
}
