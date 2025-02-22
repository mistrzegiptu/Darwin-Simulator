using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal class Fire : IWorldElement
    {
        public Vector2d Position { get; }
        public int DaysActive { get; }

        public Fire(Vector2d position)
        {
            Position = position;
        }

        public override string ToString()
        {
            return "F";
        }
    }
}
