//using System;
//using NUnit;
//using NUnit.Framework;
//using ITI.SusanooQuest.UI;
//using ITI.SusanooQuest.Lib;

//namespace ITI.SusanooQuest.Tests
//{
//    [TestFixture]
//    public class ButtonTests
//    {
//        [TestCase(int.MinValue)]
//        [TestCase(-5)]
//        [TestCase(0)]
//        public void test_invalid_width(int width)
//        {
//            Random random = new Random();
//            Vector vector = new Vector(random.Next(), random.Next());

//            Assert.Throws<ArgumentException>(() => new Button(vector, width, random.Next(1)));
//        }


//        [TestCase(int.MinValue)]
//        [TestCase(-5)]
//        [TestCase(0)]
//        public void test_invalid_height(int height)
//        {
//            Random random = new Random();
//            Vector vector = new Vector(random.Next(), random.Next());

//            Assert.Throws<ArgumentException>(() => new Button(vector, random.Next(1), height));
//        }


//        [TestCase(1, 1)]
//        [TestCase(10, 20)]
//        [TestCase(324, 124)]
//        [TestCase(int.MaxValue, int.MaxValue)]
//        public void test_valid_dimensions(int width, int height)
//        {
//            Random random = new Random();
//            Vector vector = new Vector(random.Next(int.MaxValue - width), random.Next(int.MaxValue - height));

//            Button sut = new Button(vector, width, height);

//            Assert.That(sut, Is.Not.Null);
//        }


//        [TestCase(-10, -10, false)]
//        [TestCase(7, -10, false)]
//        [TestCase(15, -10, false)]
//        [TestCase(15, 7, false)]
//        [TestCase(15, 15, false)]
//        [TestCase(7, 15, false)]
//        [TestCase(-10, 15, false)]
//        [TestCase(-10, 7, false)]

//        [TestCase(5, -10, false)]
//        [TestCase(10, -10, false)]
//        [TestCase(15, 5, false)]
//        [TestCase(15, 10, false)]
//        [TestCase(10, 15, false)]
//        [TestCase(5, 15, false)]
//        [TestCase(-10, 10, false)]
//        [TestCase(-10, 5, false)]

//        [TestCase(5, 5, true)]
//        [TestCase(10, 5, false)]
//        [TestCase(10, 10, false)]
//        [TestCase(5, 10, false)]

//        [TestCase(7, 5, true)]
//        [TestCase(10, 7, false)]
//        [TestCase(7, 10, false)]
//        [TestCase(5, 7, true)]

//        [TestCase(7, 7, true)]
//        public void test_if_position_is_in_the_button(int x, int y, bool inButton)
//        {
//            Button sut = new Button(new Vector(5, 5), 5, 5);

//            Assert.That(sut.Selected(new Vector(x, y)), Is.EqualTo(inButton));
//        }


//        [TestCase(int.MaxValue - 5, int.MaxValue - 10, 10, 5)]
//        [TestCase(int.MaxValue - 10, int.MaxValue - 5, 5, 10)]
//        [TestCase(int.MaxValue - 5, int.MaxValue - 5, 10, 10)]
//        [TestCase(int.MaxValue, int.MaxValue, 1, 1)]
//        public void test_if_position_more_width_or_heigth_is_out_of_int_values(int x, int y, int width, int heigth)
//        {
//            Vector vector = new Vector(x, y);

//            Assert.Throws<ArgumentOutOfRangeException>(() => new Button(vector, width, heigth));
//        }
//    }
//}