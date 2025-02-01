using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal interface IWorldMap
    {
        void RemoveDeadAnimals();
        void MoveAnimals();
        void EatPlants();
        void ReproduceAnimals();
        void SpawnNewPlants(int plantCount);

    }
}
