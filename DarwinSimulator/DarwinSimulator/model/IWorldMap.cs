using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal interface IWorldMap
    {
        void PassDay(int day);
        void RemoveDeadAnimals(int day);
        void MoveAnimals();
        void EatPlants();
        void ReproduceAnimals();
        void SpawnNewPlants(int plantCount);

    }
}
