using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public class FireMap : WorldMap
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
            Vector2d newFirePosition = plants.Keys.FirstOrDefault();

            _fires.Add(newFirePosition, new Fire(newFirePosition));
            plants.Remove(newFirePosition);
        }

        public void SpreadOrExtinguish()
        {
            List<IWorldElement> extinguishedFires = new();
            List<IWorldElement> spreadingFires = new();

            foreach (Fire fire in _fires.Values)
            {
                if (fire.DaysActive == parameters.WorldParameters.FireDuration)
                    extinguishedFires.Add(fire);
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
                            spreadingFires.Add(new Fire(spreadingPosition));
                            plants.Remove(spreadingPosition);
                        }
                    }
                }
            }

            extinguishedFires.ForEach(x => _fires.Remove(x.Position));
            spreadingFires.ForEach(x => _fires.Add(x.Position, x));
        }

        public override IWorldElement? ObjectAt(Vector2d position)
        {
            if(_fires.ContainsKey(position))
                return _fires[position];
            return base.ObjectAt(position);
        }
    }
}
