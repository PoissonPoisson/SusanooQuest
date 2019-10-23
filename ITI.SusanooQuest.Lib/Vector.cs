using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.SusanooQuest.Lib
{
    public struct Vector
    {
        public readonly double X;

        public readonly double Y;

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector Add(double x, double y)
        {
            return new Vector(X + x, Y + y);
        }

        public Vector Multiply(double n)
        {
            return new Vector(X * n, Y * n);
        }
    }
}
