using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public interface ICreature : IWorldElement
    {
        Genome Genome { get; }
        void Move(IMoveValidator validator, int energyLoss);
    }
}
