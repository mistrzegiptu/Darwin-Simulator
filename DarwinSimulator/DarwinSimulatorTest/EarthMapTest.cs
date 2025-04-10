using DarwinSimulator.model;
using DarwinSimulator.model.records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulatorTest
{
    public class EarthMapTest
    {
        private static readonly WorldParameters wp = new WorldParameters(10, 10, MapType.EARTH_MAP, PlanterType.EQUATOR_PLANTER, 10, 2, 1, 20, 5, 2);
        private static readonly AnimalParameters ap = new AnimalParameters(5, 3, 2, AnimalType.NORMAL_ANIMAL);
        private static readonly GenomeParameters gp = new GenomeParameters(1, 4, GenomeType.NORMAL_GENOME, 10);
        private static readonly Parameters parameters = new Parameters(ap, gp, wp);

        [Fact]
        public void RemoveDeadAnimals()
        {
            WorldMap worldMap = new EarthMap(parameters);
            foreach (var animalList in worldMap.Animals.Values)
            {
                foreach (var animal in animalList)
                {
                    animal.Die();
                }
            }
            worldMap.PassDay(1);
            Assert.Equal(wp.StartingAnimalCount, worldMap.DeadAnimals.Count);
        }

        [Fact]
        public void ChangeOnBound()
        {
            var worldMap = new EarthMap(parameters);

            Assert.True(worldMap.CanMoveTo(new Vector2d(0, 0)));
            Assert.Equal(new Vector2d(wp.Width - 1, 2), worldMap.ChangeOnBound(new Vector2d(-1, 2)));
        }

        [Fact]
        public void CanMoveTo()
        {
            var worldMap = new EarthMap(parameters);

            Assert.True(worldMap.CanMoveTo(new Vector2d(0, 0)));
            Assert.False(worldMap.CanMoveTo(new Vector2d(-1, -1)));
        }
    }
}
