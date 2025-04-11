using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public class Plant : IWorldElement
    {
        public Vector2d Position { get; }
        public Plant(Vector2d position) 
        {
            this.Position = position;
        }

        public override string ToString()
        {
            return "P";
        }

        public string GetImageFileName()
        {
            return "plant.png";
        }
    }
}
