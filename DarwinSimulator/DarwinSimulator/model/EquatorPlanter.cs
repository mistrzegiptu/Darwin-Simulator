using DarwinSimulator.model.records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public class EquatorPlanter : Planter
    {
        public EquatorPlanter(WorldMap worldMap) : base(worldMap)
        {

        }

        public override int SpawnNewPlants(Dictionary<Vector2d, IWorldElement> plants, int plantCount)
        {
            Boundary boundary = worldMap.Boundary;
            int midY = (boundary.UpperRight.Y - boundary.LowerLeft.Y) / 2;
            int equatorDeltaY = (boundary.UpperRight.Y - boundary.LowerLeft.Y) / 10;
            int actuallyPlanted = 0;

            for(int i = 0; i < plantCount; i++)
            {
                int position = rand.Next(100);
                Vector2d positionToSpawn;
                if (position < 80)
                {
                    positionToSpawn = new Vector2d(rand.Next(boundary.LowerLeft.X, boundary.UpperRight.X), rand.Next(midY - equatorDeltaY, midY + equatorDeltaY));
                }
                else
                {
                    if (rand.Next(2) == 0)
                        positionToSpawn = new Vector2d(rand.Next(boundary.LowerLeft.X, boundary.UpperRight.X), rand.Next(boundary.LowerLeft.Y, midY - equatorDeltaY));
                    else
                        positionToSpawn = new Vector2d(rand.Next(boundary.LowerLeft.X, boundary.UpperRight.X), rand.Next(midY + equatorDeltaY, boundary.UpperRight.Y));
                }

                if(worldMap.CanPlant(positionToSpawn))
                {
                    plants.Add(positionToSpawn, new Plant(positionToSpawn));
                    actuallyPlanted++;
                }
            }

            return actuallyPlanted;
        }
    }
}
