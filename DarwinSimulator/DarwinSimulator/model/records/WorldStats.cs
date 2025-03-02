using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model.records
{
    internal record WorldStats(int AllAnimalCount, int AllPlantCount, int FreeFields, string MostPopularGenotype,
                               double AverageEnergy, double AverageLifetime, double AverageChildCount);
}
