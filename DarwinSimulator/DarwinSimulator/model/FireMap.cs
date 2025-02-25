using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    internal class FireMap : WorldMap
    {
        private Dictionary<Vector2d, IWorldElement> _fires = new();

        public FireMap(Parameters parameters) : base(parameters)
        {
        }

        public override void PassDay(int day)
        {
            if (day % parameters.WorldParameters.NewFirePeriod == 0)
                CreateFire();
            SpreadOrExtinguish();
            base.PassDay(day);
        }

        public void CreateFire()
        {
            Vector2d newFirePosition = plants.Keys.First();

            _fires.Add(newFirePosition, new Fire(newFirePosition));
            plants.Remove(newFirePosition);
        }

        public void SpreadOrExtinguish()
        {
            foreach(Fire fire in _fires.Values)
            {
                if (fire.DaysActive == parameters.WorldParameters.FireDuration)
                    _fires.Remove(fire.Position);
                else
                {
                    fire.AddActiveDay();

                    if (animals.ContainsKey(fire.Position))
                        animals[fire.Position].ForEach(animal => animal.Die());

                    foreach (var direction in MapDirectionExtension.GetMainDirections())
                    {
                        Vector2d spreadingPosition = fire.Position.Add(direction.ToUnitVector());
                        if (plants.ContainsKey(spreadingPosition))
                        {
                            _fires.Add(spreadingPosition, new Fire(spreadingPosition));
                            plants.Remove(spreadingPosition);
                        }
                    }
                }
            }
        }
    }
}
