using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public class CrazyAnimal : Animal
    {
        public CrazyAnimal(Vector2d startingPosition, Parameters parameters) : base(startingPosition, parameters)
        {
        }

        public CrazyAnimal(Genome genome, Vector2d position, int energy, Parameters parameters) : base(genome, position, energy, parameters)
        {
        }

        public override void Move(IMoveValidator moveValidator, int energyLoss = 1)
        {
            base.Move(moveValidator, energyLoss);

            int chanceToAnotherMove = rand.Next(100);

            if (chanceToAnotherMove < 20)
                Genome.JumpToRandom();
            else
                base.Move(moveValidator, energyLoss);  
        }
    }
}
