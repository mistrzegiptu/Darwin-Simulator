using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DarwinSimulator.model
{
    internal enum MapDirection
    {
        NORTH, NORTH_EAST, EAST, SOUTH_EAST, SOUTH, SOUTH_WEST, WEST, NORTH_WEST
    }
    internal static class MapDirectionExtension
    {
        public static int GetLength() => Enum.GetValues(typeof(MapDirection)).Length;
        public static Vector2d ToUnitVector(this MapDirection direction)
        {
            return direction switch
            {
                MapDirection.NORTH => new Vector2d(0, 1),
                MapDirection.NORTH_EAST => new Vector2d(1, 1),
                MapDirection.EAST => new Vector2d(1, 0),
                MapDirection.SOUTH_EAST => new Vector2d(1, -1),
                MapDirection.SOUTH => new Vector2d(0, -1),
                MapDirection.SOUTH_WEST => new Vector2d(-1, -1),
                MapDirection.WEST => new Vector2d(-1, 0),
                MapDirection.NORTH_WEST => new Vector2d(-1, 1),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), "Invalid direction")
            };
        }

        public static MapDirection Rotate(this MapDirection direction, int rotations)
        {
            int length = GetLength();
            int newValue = ((int)direction + rotations) % length;

            if (newValue < 0)
                newValue += length;

            return (MapDirection)newValue;
        }

        public static MapDirection Reverse(this MapDirection direction)
        {
            return direction.Rotate(GetLength() / 2);
        }

        public static List<MapDirection> GetMainDirections()
        {
            return new List<MapDirection> { MapDirection.NORTH, MapDirection.EAST, MapDirection.SOUTH, MapDirection.WEST };
        }

        public static List<MapDirection> GetAllDirections()
        {
            return new List<MapDirection> { MapDirection.NORTH, MapDirection.NORTH_EAST, MapDirection.EAST, MapDirection.SOUTH_EAST, 
                                            MapDirection.SOUTH, MapDirection.SOUTH_WEST, MapDirection.WEST, MapDirection.NORTH_WEST };
        }

        public static String ToString(this MapDirection direction)
        {
            return direction switch
            {
                MapDirection.NORTH => "N",
                MapDirection.NORTH_EAST => "NE",
                MapDirection.EAST => "E",
                MapDirection.SOUTH_EAST => "SE",
                MapDirection.SOUTH => "S",
                MapDirection.SOUTH_WEST => "SW",
                MapDirection.WEST => "W",
                MapDirection.NORTH_WEST => "NW",
                _ => throw new ArgumentOutOfRangeException(nameof(direction), "Invalid direction")
            };
        }
    }
}
