using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public class Fire : IWorldElement
    {
        public Vector2d Position { get; }
        public int DaysActive { get; private set; }

        public Fire(Vector2d position)
        {
            Position = position;
        }

        public void AddActiveDay()
        {
            DaysActive++;
        }

        public override string ToString()
        {
            return "F";
        }
    }
}
