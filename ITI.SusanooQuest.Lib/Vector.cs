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

        public Vector Multiply(float n)
        {
            return new Vector(X * n, Y * n);
        }

        public float Distance(Vector other)
        {
            return (float)Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }
    }
}
