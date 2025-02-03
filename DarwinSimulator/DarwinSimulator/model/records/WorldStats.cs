using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model.records
{
    internal record WorldStats(int AllAnimalCount, int AllPlantCount, int FreeFields, string MostPopularGenotype,
                               int AverageEnergy, int AverageLifetime, int AverageChildCount);
}
