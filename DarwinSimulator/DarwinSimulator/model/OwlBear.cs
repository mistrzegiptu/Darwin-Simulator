using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public class OwlBear : ICreature
    {
        public Vector2d Position { get; private set; }
        public Genome Genome { get; }
        private MapDirection _direction;

        public OwlBear(Vector2d startingPosition, Parameters parameters)
        {
            Position = startingPosition;
            Genome = GenomeFactory.CreateGenome(parameters);
        }

        public void Move(IMoveValidator validator, int energyLoss = 0)
        {
            _direction = _direction.Rotate(Genome.GetNext());
            Vector2d unitVector = _direction.ToUnitVector();

            if(validator.CanMoveTo(Position.Add(unitVector), this))
                Position = Position.Add(unitVector);
            else
            {
                _direction = _direction.Reverse();
                unitVector = _direction.ToUnitVector();
                Position = Position.Add(unitVector);
            }
            
            Position = validator.ChangeOnBound(Position, this);
        }

        public string GetImageFileName()
        {
            return "owlbear.png";
        }
    }
}
