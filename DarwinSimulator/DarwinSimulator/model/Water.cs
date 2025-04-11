using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace DarwinSimulator.model
{
    public class Water : IWorldElement
    {
        public Vector2d Position { get; }

        private int highTideCycle = 0;
        private bool raising = true;

        private List<Vector2d> firstHighTide = new List<Vector2d>();
        private List<Vector2d> secondHighTide = new List<Vector2d>();

        public Water(Vector2d position)
        {
            Position = position;

            firstHighTide.Add(position);
            secondHighTide.Add(position);

            foreach (var direction in MapDirectionExtension.GetMainDirections())
            {
                Vector2d newPosition = position.Add(direction.ToUnitVector());
                Vector2d nextNewPosition = newPosition.Add(direction.ToUnitVector());

                firstHighTide.Add(newPosition);
                secondHighTide.Add(nextNewPosition);
            }

            foreach (var direction in MapDirectionExtension.GetAllDirections())
            {
                Vector2d newPosition = position.Add(direction.ToUnitVector());

                secondHighTide.Add(newPosition);
            }

        }

        public List<Vector2d> GetNextHighTide()
        {
            var highTide = highTideCycle switch
            {
                0 => new List<Vector2d>() { Position },
                1 => firstHighTide,
                2 => secondHighTide,
                _ => throw new NotImplementedException()
            };

            Cycle();

            return highTide;
        }

        private void Cycle()
        {
            if (raising)
            {
                highTideCycle++;

                if (highTideCycle == 2)
                    raising = false;
            }
            else
            {
                highTideCycle--;

                if (highTideCycle == 0)
                    raising = true;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Water other)
                return false;

            return this.Position == other.Position;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position.X, Position.Y);
        }

        public override string ToString()
        {
            return "W";
        }

        public string GetImageFileName()
        {
            return "water.png";
        }
    }
}
