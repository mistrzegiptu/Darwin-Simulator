using DarwinSimulator.model.records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal class RipeCropPlanter : Planter
    {
        private readonly Boundary ripeCropSquare;
        public RipeCropPlanter(WorldMap worldMap) : base(worldMap)
        {
            Boundary boundary = worldMap.Boundary;

            int squareSize = Math.Min(((boundary.UpperRight.X - boundary.LowerLeft.X) / 5), ((boundary.UpperRight.Y - boundary.LowerLeft.Y) / 5));
            int spawnBoundaryX = boundary.UpperRight.X - squareSize - 1;
            int spawnBoundaryY = boundary.UpperRight.Y - squareSize - 1;

            Vector2d lowerLeft = new Vector2d(rand.Next(boundary.LowerLeft.X, spawnBoundaryX), rand.Next(boundary.LowerLeft.Y, spawnBoundaryY));
            Vector2d upperRight = lowerLeft.Add(new Vector2d(squareSize, squareSize));

            ripeCropSquare = new Boundary(lowerLeft, upperRight);
        }

        public override int SpawnNewPlants(Dictionary<Vector2d, IWorldElement> plants, int plantCount)
        {
            int actuallyPlanted = 0;

            for(int i = 0; i < plants.Count; i++)
            {
                if(rand.Next(100) < 80)
                {
                    int randomX = rand.Next(ripeCropSquare.LowerLeft.X, ripeCropSquare.UpperRight.X);
                    int randomY = rand.Next(ripeCropSquare.LowerLeft.Y, ripeCropSquare.UpperRight.Y);
                    Vector2d randomPosition = new Vector2d(randomX, randomY);

                    BigPlant plant = new BigPlant(randomPosition);
                    foreach(var position in plant.CoveredPositions)
                        plants.TryAdd(position, plant);
                }
                else
                {
                    Boundary boundary = worldMap.Boundary;
                    Vector2d spawnPosition = new Vector2d(rand.Next(boundary.LowerLeft.X, boundary.UpperRight.X), rand.Next(boundary.LowerLeft.Y, boundary.UpperRight.Y));

                    while(spawnPosition.Follows(ripeCropSquare.LowerLeft) && spawnPosition.Precedes(ripeCropSquare.UpperRight))
                    {
                        spawnPosition = new Vector2d(rand.Next(boundary.LowerLeft.X, boundary.UpperRight.X), rand.Next(boundary.LowerLeft.Y, boundary.UpperRight.Y));
                    }

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