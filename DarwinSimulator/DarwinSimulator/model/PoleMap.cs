using DarwinSimulator.model.records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public class PoleMap : WorldMap
    {
        private Boundary topPole, bottomPole;
        public PoleMap(Parameters parameters) : base(parameters)
        {
            Vector2d lowerLeft = Boundary.LowerLeft;
            Vector2d upperRight = Boundary.UpperRight;
            int deltaY = (Boundary.UpperRight.Y - Boundary.LowerLeft.Y) / 10;

            topPole = new Boundary(new Vector2d(lowerLeft.X, lowerLeft.Y), new Vector2d(upperRight.X, lowerLeft.Y + deltaY));
            bottomPole = new Boundary(new Vector2d(lowerLeft.X, upperRight.Y - deltaY), new Vector2d(upperRight.X, upperRight.Y));
        }

        public override void MoveAnimals()
        {
            foreach (var animalsOnField in animals.Values)
            {
                foreach (var animal in animalsOnField)
                {
                    int poleLossMultiplier = GetPoleMultiplier(animal.Position);
                    animal.Move(this, poleLossMultiplier);
                }
            }
        }

        private int GetPoleMultiplier(Vector2d position)
        {
            if (position.Follows(topPole.LowerLeft) && position.Precedes(topPole.UpperRight))
            {
                if (Math.Abs(position.Y - topPole.LowerLeft.Y) < Math.Abs(position.Y - topPole.UpperRight.Y))
                    return 2;
                else
                    return 3;
            }
            else if (position.Follows(bottomPole.LowerLeft) && position.Precedes(bottomPole.UpperRight))
            {
                if (Math.Abs(position.Y - bottomPole.LowerLeft.Y) > Math.Abs(position.Y - bottomPole.UpperRight.Y))
                    return 2;
                else
                    return 3;
            }
            else
                return 1;
        }
    }
}
