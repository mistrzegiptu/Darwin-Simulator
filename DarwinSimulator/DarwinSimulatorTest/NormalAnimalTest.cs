using DarwinSimulator.model.records;
using DarwinSimulator.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulatorTest
{
    public class NormalAnimalTest
    {

        [Fact]
        public void ShouldReproduce()
        {
            WorldParameters wp = new WorldParameters(10, 10, MapType.EARTH_MAP, PlanterType.EQUATOR_PLANTER, 10, 2, 1, 20, 5, 2);
            AnimalParameters ap = new AnimalParameters(5, 3, 2, AnimalType.NORMAL_ANIMAL);
            GenomeParameters gp = new GenomeParameters(1, 4, GenomeType.NORMAL_GENOME, 10);
            Parameters parameters = new Parameters(ap, gp, wp);

            Animal first = AnimalFactory.CreateAnimal(new Vector2d(0, 0), parameters);
            Animal second = AnimalFactory.CreateAnimal(new Vector2d(0, 0), parameters);

            Animal child;
            Assert.True(first.TryReproduce(second, out child));
            Assert.NotNull(child);
            Assert.Equal(ap.EnergyUsedForReproducing * 2, child.Energy);
            Assert.Equal(ap.StartingEnergyLevel - ap.EnergyUsedForReproducing, first.Energy);
            Assert.Equal(ap.StartingEnergyLevel - ap.EnergyUsedForReproducing, first.Energy);
        }

        [Fact]
        public void ShouldNotReproduce()
        {
            WorldParameters wp = new WorldParameters(10, 10, MapType.EARTH_MAP, PlanterType.EQUATOR_PLANTER, 10, 2, 1, 20, 5, 2);
            AnimalParameters ap = new AnimalParameters(5, 10, 2, AnimalType.NORMAL_ANIMAL);
            GenomeParameters gp = new GenomeParameters(1, 4, GenomeType.NORMAL_GENOME, 10);
            Parameters parameters = new Parameters(ap, gp, wp);

            Animal first = AnimalFactory.CreateAnimal(new Vector2d(0, 0), parameters);
            Animal second = AnimalFactory.CreateAnimal(new Vector2d(0, 0), parameters);

            Animal child;
            Assert.False(first.TryReproduce(second, out child));
            Assert.Null(child);
            Assert.Equal(ap.StartingEnergyLevel, first.Energy);
            Assert.Equal(ap.StartingEnergyLevel, first.Energy);
        }
    }
}
