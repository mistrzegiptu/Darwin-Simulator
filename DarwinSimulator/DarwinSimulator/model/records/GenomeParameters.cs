using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model.records
{
    internal record GenomeParameters(int MinMutationCount, int MaxMutationCount, GenomeType GenomeType,
                                     int GenomeLength);
}
