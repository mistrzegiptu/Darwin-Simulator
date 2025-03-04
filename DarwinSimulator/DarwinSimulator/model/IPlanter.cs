using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal interface IPlanter
    {
        int SpawnNewPlants(Dictionary<Vector2d, IWorldElement> plants, int plantCount);
    }
}
