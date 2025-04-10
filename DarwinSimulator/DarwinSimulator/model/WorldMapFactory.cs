using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public class WorldMapFactory
    {
        public static WorldMap CreateWorld(Parameters parameters)
        {
            switch (parameters.WorldParameters.MapType)
            {
                case MapType.EARTH_MAP:
                    return new EarthMap(parameters);
                case MapType.POLE_MAP:
                    return new PoleMap(parameters);
                case MapType.FIRE_MAP:
                    return new FireMap(parameters);
                case MapType.WATER_MAP:
                    return new WaterMap(parameters);
                case MapType.WILD_OWL_BEAR_MAP:
                    return new WildOwlBearMap(parameters);
                default:
                    throw new ArgumentException();
            }
        }
    }
}
