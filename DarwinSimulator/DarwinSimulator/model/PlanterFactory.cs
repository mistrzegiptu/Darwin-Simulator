using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal class PlanterFactory
    {
        public static IPlanter createPlanter(Parameters parameters, WorldMap worldMap)
        {
            switch(parameters.WorldParameters.PlanterType)
            {
                case PlanterType.EQUATOR_PLANTER:
                    return new EquatorPlanter(worldMap);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
