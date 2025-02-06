using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal class AgeingAnimal : Animal
    {
        private readonly int seniorAge = 60;
        public AgeingAnimal(Vector2d startingPosition, Parameters parameters) : base(startingPosition, parameters)
        {
        }

        public AgeingAnimal(Genome genome, Vector2d position, int energy, Parameters parameters) : base(genome, position, energy, parameters)
        {
        }

        public override void Move(MoveValidator moveValidator)
        {
            int chanceToSkipMove = rand.Next(100);
            int maxChanceToSkipMove = 80 * age / seniorAge;
            maxChanceToSkipMove = maxChanceToSkipMove > 80 ? 80 : maxChanceToSkipMove;

            if(chanceToSkipMove < maxChanceToSkipMove)
            {
                loseEnergy(1);
                age++;
            }
            else
                base.Move(moveValidator);
        }
    }
}
