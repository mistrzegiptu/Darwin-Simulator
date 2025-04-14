using DarwinSimulator.model.records;

namespace DarwinSimulator.model
{
    public class WildOwlBearMap : WorldMap
    {
        private readonly Boundary _owlBearBoundary;
        private OwlBear _owlBear;
        public WildOwlBearMap(Parameters parameters) : base(parameters)
        {
            int squareSize = Math.Min(((Boundary.UpperRight.X - Boundary.LowerLeft.X) / 5), ((Boundary.UpperRight.Y - Boundary.LowerLeft.Y) / 5));
            int spawnBoundaryX = Boundary.UpperRight.X - squareSize - 1;
            int spawnBoundaryY = Boundary.UpperRight.Y - squareSize - 1;

            Vector2d lowerLeft = new Vector2d(rand.Next(Boundary.LowerLeft.X, spawnBoundaryX), rand.Next(Boundary.LowerLeft.Y, spawnBoundaryY));
            Vector2d upperRight = lowerLeft.Add(new Vector2d(squareSize, squareSize));

            _owlBearBoundary = new Boundary(lowerLeft, upperRight);

            int owlBearX = rand.Next(squareSize);
            int owlBearY = rand.Next(squareSize);

            _owlBear = new OwlBear(new Vector2d(owlBearX, owlBearY), parameters);
        }

        private void MoveOwlBear()
        {
            _owlBear.Move(this);

            if (animals.ContainsKey(_owlBear.Position))
            {
                animals[_owlBear.Position].ForEach(x => x.Die());
            }
            
        }

        public override void PassDay(int day)
        {
            MoveOwlBear();
            base.PassDay(day);
        }

        public override void MoveAnimals()
        {
            base.MoveAnimals();

            if (animals.ContainsKey(_owlBear.Position))
            {
                animals[_owlBear.Position].ForEach(x => x.Die());
            }
        }

        public override bool CanMoveTo(Vector2d position, ICreature callerCreature)
        {
            if (callerCreature is not OwlBear)
                return base.CanMoveTo(position, callerCreature);

            return position.Y <= _owlBearBoundary.UpperRight.Y && position.Y >= _owlBearBoundary.LowerLeft.Y;
        }

        public override Vector2d ChangeOnBound(Vector2d position, ICreature callerCreature)
        {
            if (callerCreature is not OwlBear) 
                return base.ChangeOnBound(position, callerCreature);

            if (position.X == _owlBearBoundary.LowerLeft.X - 1)
                return new Vector2d(_owlBearBoundary.UpperRight.X, position.Y);

            return position.X == _owlBearBoundary.UpperRight.X + 1 ? new Vector2d(_owlBearBoundary.LowerLeft.X, position.Y) : position;
        }

        public override IWorldElement? ObjectAt(Vector2d position)
        {
            if (_owlBear is null)
                return base.ObjectAt(position);
            return _owlBear.Position == position ? _owlBear : base.ObjectAt(position);
        }
    }
}