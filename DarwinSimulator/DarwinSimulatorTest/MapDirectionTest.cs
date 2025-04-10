using DarwinSimulator.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulatorTest
{
    public class MapDirectionTest
    {
        [Fact]
        public void Rotate()
        {
            MapDirection direction = MapDirection.NORTH;

            Assert.Equal(MapDirection.EAST, direction.Rotate(2));
            Assert.Equal(MapDirection.SOUTH, direction.Rotate(4));
            Assert.Equal(MapDirection.WEST, direction.Rotate(6));
            Assert.Equal(MapDirection.NORTH, direction.Rotate(8));

            Assert.Equal(MapDirection.NORTH_EAST, direction.Rotate(1));
            Assert.Equal(MapDirection.SOUTH_EAST, direction.Rotate(3));
            Assert.Equal(MapDirection.SOUTH_WEST, direction.Rotate(5));
            Assert.Equal(MapDirection.NORTH_WEST, direction.Rotate(7));
        }

        [Fact]
        public void Reverse()
        {
            MapDirection direction = MapDirection.NORTH;

            Assert.Equal(MapDirection.SOUTH, direction.Reverse());
        }
    }
}
