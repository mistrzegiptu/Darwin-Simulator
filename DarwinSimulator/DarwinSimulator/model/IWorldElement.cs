using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public interface IWorldElement
    {
        Vector2d Position { get; }
        string GetImageFileName();
    }
}
