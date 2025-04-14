using DarwinSimulator.model.records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public class CorpsePlanter : Planter
    {
        List<Vector2d> corpses = new();
        public CorpsePlanter(WorldMap worldMap) : base(worldMap)
        {
            worldMap.AnimalDied += OnAnimalDied;
        }

        public override int SpawnNewPlants(Dictionary<Vector2d, IWorldElement> plants, int plantCount)
        {
            int actuallyPlanted = 0;

            for(int i = 0; i < plantCount; i++)
            {
                if (corpses.Count > 0 && rand.Next(100) < 80)
                {
                    Vector2d basePosition = corpses[0];
                    
                    foreach (var direction in MapDirectionExtension.GetMainDirections())
                    {
                        Vector2d spawnPosition = basePosition.Add(direction.ToUnitVector());
                        if (worldMap.CanPlant(spawnPosition))
                        {
                            plants.Add(spawnPosition, new Plant(spawnPosition));
                            actuallyPlanted++;
                        }
                    }

                    i += 8;
                    corpses.RemoveAt(0);
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

        private void OnAnimalDied(Vector2d position)
        {
            corpses.Add(position);
        }
    }
}
