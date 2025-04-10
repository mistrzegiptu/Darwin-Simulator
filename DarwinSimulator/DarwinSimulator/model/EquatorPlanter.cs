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
            int midX = (boundary.UpperRight.X - boundary.LowerLeft.X) / 2;
            int equatorDeltaX = (boundary.UpperRight.X - boundary.LowerLeft.X) / 10;
            int actuallyPlanted = 0;

            for(int i = 0; i < plantCount; i++)
            {
                int position = rand.Next(100);
                Vector2d positionToSpawn;
                if (position < 80)
                {
                    positionToSpawn = new Vector2d(rand.Next(midX - equatorDeltaX, midX + equatorDeltaX), rand.Next(boundary.LowerLeft.Y, boundary.UpperRight.Y));
                }
                else
                {
                    if (rand.Next(1) == 0)
                        positionToSpawn = new Vector2d(rand.Next(boundary.LowerLeft.X, midX - equatorDeltaX), rand.Next(boundary.LowerLeft.Y, boundary.UpperRight.Y));
                    else
                        positionToSpawn = new Vector2d(rand.Next(midX + equatorDeltaX, boundary.UpperRight.X), rand.Next(boundary.LowerLeft.Y, boundary.UpperRight.Y));
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
