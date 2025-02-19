using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal abstract class Planter : IPlanter
    {
        protected readonly Random rand = new Random();
        protected WorldMap worldMap;

        public Planter(WorldMap worldMap) 
        {
            this.worldMap = worldMap;
        }

        public abstract void SpawnNewPlants(Dictionary<Vector2d, IWorldElement> plants, int plantCount);
    }
}
