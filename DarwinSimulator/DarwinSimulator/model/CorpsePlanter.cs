using DarwinSimulator.model.records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal class CorpsePlanter : Planter
    {
        List<Vector2d> corpses = new();
        public CorpsePlanter(WorldMap worldMap) : base(worldMap)
        {
            worldMap.AnimalDied += OnAnimalDied;
        }

        public override void SpawnNewPlants(Dictionary<Vector2d, IWorldElement> plants, int plantCount)
        {
            for(int i = 0; i < plants.Count; i++)
            {
                if (corpses.Count > 0 && rand.Next(100) < 80)
                {
                    Vector2d basePosition = corpses[0];
                    
                    foreach (var direction in MapDirectionExtension.GetMainDirections())
                    {
                        Vector2d spawnPosition = basePosition.Add(direction.ToUnitVector());
                        plants.TryAdd(spawnPosition, new Plant(spawnPosition));
                    }

                    i += 8;
                    corpses.RemoveAt(0);
                }
                else
                {
                    Boundary boundary = worldMap.Boundary;
                    
                    int randX = rand.Next(boundary.LowerLeft.X, boundary.UpperRight.X);
                    int randY = rand.Next(boundary.LowerLeft.Y, boundary.UpperRight.Y);
                    Vector2d position = new Vector2d(randX, randY);

                    plants.TryAdd(position, new Plant(position));
                }
            }
        }

        private void OnAnimalDied(Vector2d position)
        {
            corpses.Add(position);
        }
    }
}
