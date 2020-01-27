using ITI.SusanooQuest.Lib;
using System;
using NUnit.Framework;

namespace ITI.SusanooQuest.Tests
{
    [TestFixture]
    public class CollisionTest
    {
        [TestCase(0f, 0f, 0f, 1f, 0f, .5f, 1f)]
        public void test_collision_algo_true(float oriX, float oriY, float desX, float desY, float pX, float pY, float pR)
        { 
            Assert.That(Collision(new Vector(oriX, oriY), new Vector(desX, desY), new Vector(pX, pY), pR), Is.True);
        }

        [TestCase(0f, 0f, 0f, 1f, 10f, 10f, 1f)]
        [TestCase(0f, 0f, 10f, 0f, 4f, 2f, 2f)]
        public void test_collision_algo_false(float oriX, float oriY, float desX, float desY, float pX, float pY, float pR)
        {
            Assert.That(Collision(new Vector(oriX, oriY), new Vector(desX, desY), new Vector(pX, pY), pR), Is.False);
        }

        public bool Collision(Vector origin, Vector destination, Vector player, float playerRadius)
        {
            Vector originDestination = new Vector(destination.X - origin.X, destination.Y - origin.Y);
            Vector originPlayer = new Vector(player.X - origin.X, player.Y - origin.Y);

            float numerator = originDestination.X * originPlayer.Y - originDestination.Y * originPlayer.X;

            if (numerator < 0) numerator *= -1;

            float denominator = (float)Math.Sqrt(originDestination.X * originDestination.X + originDestination.Y * originDestination.Y);
            float CI = numerator / denominator;

            return CI < playerRadius;
        }
    }
}
