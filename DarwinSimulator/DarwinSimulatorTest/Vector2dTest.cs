using DarwinSimulator.model;

namespace DarwinSimulatorTest
{
    public class Vector2dTest
    {
        [Fact]
        public void TestEquals()
        {
            var v1 = new Vector2d(1, 2);
            var v2 = new Vector2d(1, 2);
            var v3 = new Vector2d(2, 1);

            Assert.Equal(v1, v2);
            Assert.NotEqual(v1, v3);
        }

        [Fact]
        public void TestToString()
        {
            var v1 = new Vector2d(10, 20);

            Assert.Equal("(10, 20)", v1.ToString());
        }

        [Fact]
        public void Precedes()
        {
            var v1 = new Vector2d(1, 1);
            var v2 = new Vector2d(2, 2);

            Assert.True(v1.Precedes(v2));
        }

        [Fact]
        public void Follows()
        {
            var v1 = new Vector2d(2, 2);
            var v2 = new Vector2d(1, 1);

            Assert.True(v1.Follows(v2));
        }

        [Fact]
        public void UpperRight()
        {
            var v1 = new Vector2d(1, 2);
            var v2 = new Vector2d(2, 1);

            Assert.Equal(new Vector2d(2, 2), v1.UpperRight(v2));
        }

        [Fact]
        public void LowerLeft()
        {
            var v1 = new Vector2d(1, 2);
            var v2 = new Vector2d(2, 1);

            Assert.Equal(new Vector2d(1, 1), v1.LowerLeft(v2));
        }

        [Fact]
        public void Add()
        {
            var v1 = new Vector2d(1, 2);
            var v2 = new Vector2d(-1, -2);

            Assert.Equal(new Vector2d(0, 0), v1.Add(v2));
        }

        [Fact]
        public void Subtract()
        {
            var v1 = new Vector2d(1, 2);
            var v2 = new Vector2d(2, 3);

            Assert.Equal(new Vector2d(-1, -1), v1.Subtract(v2));
        }

        [Fact]
        public void Opposite()
        {
            var v1 = new Vector2d(0, 0);

            Assert.Equal(new Vector2d(0, 0), v1.Opposite());
        }
    }
}