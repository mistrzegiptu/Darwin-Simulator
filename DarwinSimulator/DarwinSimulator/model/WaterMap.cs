using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public class WaterMap : WorldMap
    {
        private readonly Dictionary<Vector2d, IWorldElement> _waters = new();
        private readonly List<IWorldElement> _startingWaters = new();
        private readonly static Dictionary<Vector2d, IWorldElement> _waterCache = new();
        public WaterMap(Parameters parameters) : base(parameters)
        {
            int worldWidth = parameters.WorldParameters.Width;
            int worldHeight = parameters.WorldParameters.Height;
            int watersCount = worldWidth * worldHeight / 100;

            for (int i = 0; i < watersCount; i++)
            {
                int waterX = rand.Next(worldWidth);
                int waterY = rand.Next(worldHeight);

                Vector2d waterPosition = new Vector2d(waterX, waterY);
                if (_startingWaters.Contains(new Water(waterPosition)))
                {
                    i -= 1;
                    continue;
                }

                _startingWaters.Add(new Water(waterPosition));
            }
        }

        public override void PassDay(int day)
        {
            WaterTides();
            base.PassDay(day);
        }

        private void WaterTides()
        {
            _waters.Clear();

            foreach (Water water in _startingWaters)
            {
                foreach(var position in water.GetNextHighTide())
                {
                    if (animals.ContainsKey(position))
                        animals[position].ForEach(x => x.Die());
                    else if (plants.ContainsKey(position))
                        plants.Remove(position);

                    if (!_waterCache.ContainsKey(position))
                        _waterCache[position] = new Water(position);
                    _waters.TryAdd(position, _waterCache[position]);
                }
            }
        }

        public override IWorldElement? ObjectAt(Vector2d position)
        {
            if (_waters.ContainsKey(position))
                return _waters[position];
            return base.ObjectAt(position);
        }
    }
}
