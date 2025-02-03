using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal interface IWorldElement
    {
        Vector2d Position { get; }
        string ToString();
    }
}
