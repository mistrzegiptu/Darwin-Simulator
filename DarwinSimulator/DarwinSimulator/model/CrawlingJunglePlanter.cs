using DarwinSimulator.model.records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public class CrawlingJunglePlanter : Planter
    {
        public CrawlingJunglePlanter(WorldMap worldMap) : base(worldMap)
        {

        }

        public override int SpawnNewPlants(Dictionary<Vector2d, IWorldElement> plants, int plantCount)
        {
            int actuallyPlanted = 0;

            for(int i = 0; i < plants.Count; i++)
            {
                if(plants.Keys.Count > 0 && rand.Next(100) < 80)
                {
                    Vector2d basePosition = plants.Keys.ToList()[rand.Next(plants.Keys.Count)];

                    foreach (var direction in MapDirectionExtension.GetMainDirections())
                    {
                        Vector2d spawnPosition = basePosition.Add(direction.ToUnitVector());
                        if(worldMap.CanPlant(spawnPosition))
                        {
                            plants.Add(spawnPosition, new Plant(spawnPosition));
                            actuallyPlanted++;
                        }
                    }

                    i += 8;
                }
                else
                {
                    Boundary boundary = worldMap.Boundary;

                    int randX = rand.Next(boundary.LowerLeft.X, boundary.UpperRight.X);
                    int randY = rand.Next(boundary.LowerLeft.Y, boundary.UpperRight.Y);
                    Vector2d spawnPosition = new Vector2d(randX, randY);

                    if (worldMap.CanPlant(spawnPosition))
                    {
                        plants.Add(spawnPosition, new Plant(spawnPosition));
                        actuallyPlanted++;
                    }
                }
            }

            return actuallyPlanted;
        }
    }
}
