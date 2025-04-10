﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinSimulator.model
{
    public struct Vector2d
    {
        public int X { get; }
        public int Y { get; }

        public Vector2d(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Precedes(Vector2d other)
        {
            return X <=  other.X && Y <= other.Y;
        }

        public bool Follows(Vector2d other)
        {
            return X >= other.X && Y >= other.Y;
        }

        public Vector2d Add(Vector2d other)
        {
            return new Vector2d(X + other.X, Y + other.Y);
        }

        public Vector2d Subtract(Vector2d other)
        {
            return new Vector2d(X - other.X, Y - other.Y);
        }

        public Vector2d UpperRight(Vector2d other)
        {
            return new Vector2d(Math.Max(X, other.X), Math.Max(Y, other.Y));
        }

        public Vector2d LowerLeft(Vector2d other)
        {
            return new Vector2d(Math.Min(X, other.X), Math.Min(Y, other.Y));
        }

        public Vector2d Opposite()
        {
            return new Vector2d(-X, -Y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Vector2d left, Vector2d right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Vector2d left, Vector2d right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
