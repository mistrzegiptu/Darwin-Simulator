using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public interface IMoveValidator
    {
        bool CanMoveTo(Vector2d position, ICreature callerCreature);
        Vector2d ChangeOnBound(Vector2d position, ICreature callerCreature);
    }
}
