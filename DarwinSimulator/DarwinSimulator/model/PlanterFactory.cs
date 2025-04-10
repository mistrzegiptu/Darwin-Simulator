using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public class PlanterFactory
    {
        public static IPlanter CreatePlanter(Parameters parameters, WorldMap worldMap)
        {
            switch(parameters.WorldParameters.PlanterType)
            {
                case PlanterType.EQUATOR_PLANTER:
                    return new EquatorPlanter(worldMap);
                case PlanterType.CORPSE_PLANTER:
                    return new CorpsePlanter(worldMap);
                case PlanterType.RIPE_CROP_PLANTER:
                    return new RipeCropPlanter(worldMap);
                case PlanterType.CRAWLING_JUNGLE_PLANTER:
                    return new CrawlingJunglePlanter(worldMap);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
