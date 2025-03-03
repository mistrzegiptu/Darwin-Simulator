using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model.records
{
    public record AnimalStats(string Genome, int ActivatedPart, int Energy, int PlantsEaten,
                                int ChildCount, int DescendantCount, bool IsAlive, int AgeOrDeathDay);
}
