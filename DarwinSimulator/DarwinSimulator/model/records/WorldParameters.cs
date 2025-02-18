using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model.records
{
    internal record WorldParameters(int Width, int Height, MapType MapType, PlanterType PlanterType, int StartingPlantCount, int EnergyForEating,
                                    int DailyPlantGrow, int StartingAnimalCount, int NewFirePeriod, int FireDuration);
}
