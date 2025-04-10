using DarwinSimulator.model.records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public interface IWorldMap
    {
        WorldStats WorldStats { get; }

        void PassDay(int day);
        void RemoveDeadAnimals(int day);
        void MoveAnimals();
        void EatPlants();
        void ReproduceAnimals();
        void SpawnNewPlants(int plantCount);
        IWorldElement? ObjectAt(Vector2d position);
        bool CanPlant(Vector2d position);
    }
}
