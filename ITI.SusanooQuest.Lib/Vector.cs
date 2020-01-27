using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    public struct Vector
    {
        public readonly float X;

        public readonly float Y;

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector Add(float x, float y)
        {
            return new Vector(X + x, Y + y);
        }

        public Vector Add(Vector other)
        {
            return new Vector(X + other.X, Y + other.Y);
        }

        public Vector Sub(Vector other)
        {
            return new Vector(X - other.X, Y - other.Y);
        }

        public Vector Multiply(float n)
        {
            return new Vector(X * n, Y * n);
        }

        public float Distance(Vector other)
        {
            return (float)Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }

        public static bool operator ==(Vector vector1, Vector vector2)
        {
            return vector1.X == vector2.X && vector1.Y == vector2.Y;
        }

        public static bool operator !=(Vector vector1, Vector vector2)
        {
            return vector1.X != vector2.X && vector1.Y != vector2.Y;
        }
    }
}
