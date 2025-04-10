using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model.records
{
    public record AnimalParameters(int StartingEnergyLevel, int MinEnergyForReproducing, int EnergyUsedForReproducing, AnimalType AnimalType);
}
