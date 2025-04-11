using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public class BigPlant : IWorldElement
    {
        public int EnergyMultiplier { get; } = 2;
        public Vector2d Position { get; }
        public List<Vector2d> CoveredPositions { get; }

        public BigPlant(Vector2d rootPosition)
        {
            Position = rootPosition;

            CoveredPositions = new List<Vector2d>() { rootPosition, rootPosition.Add(new Vector2d(1, 0)), 
                             rootPosition.Add(new Vector2d(0, 1)), rootPosition.Add(new Vector2d(1, 1)) };
        }

        public override string ToString()
        {
            return "B";
        }

        public string GetImageFileName()
        {
            return "plant.png";
        }
    }
}
