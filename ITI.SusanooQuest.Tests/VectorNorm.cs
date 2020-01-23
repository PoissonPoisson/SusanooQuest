using ITI.SusanooQuest.Lib;
using System;
using NUnit.Framework;

namespace ITI.SusanooQuest.Tests
{
    [TestFixture]
    public class VectorNorm
    {
         //[TestCase(new Vector(0, 0), new Vector(1, 0)]
         [TestCase(0, 0, 1, 0)]
         //public void test_if_two_methodes_for_norme_are_equals(Vector vector1, Vector vector2)
         public void test_if_two_methodes_for_norme_are_equals(int x1, int y1, int x2, int y2)
        {
            Vector vector1 = new Vector(x1, y1);
            Vector vector2 = new Vector(x2, y2);

            Assert.That(
                Math.Sqrt(Math.Pow(1 / (vector2.X - vector1.X), 2) + Math.Pow(1 / (vector2.Y - vector1.Y), 2)),
                Is.EqualTo(
                    Math.Sqrt(
                        Math.Pow(Math.Cos(Math.Atan2(vector2.Y - vector1.Y, vector2.X - vector1.X)), 2) +
                        Math.Pow(Math.Sin(Math.Atan2(vector2.Y - vector1.Y, vector2.X - vector1.X)), 2)
                    )
                )
            );
            //Assert.That(Math.Sqrt(Math.Pow(1 / (vector2.X - vector1.X), 2) + Math.Pow(1 / (vector2.Y - vector1.Y), 2)), Is.EqualTo(1d));
        }
    }
}
