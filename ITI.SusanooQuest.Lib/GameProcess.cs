using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace ITI.SusanooQuest.Lib
{

public class GameProcess
    { 
        public static void Serialization()
        {

            // Creates a new TestSimpleObject object.
            TestSimpleObject obj = new TestSimpleObject();

            Console.WriteLine("Before serialization the object contains: ");
            obj.Print();

            // Opens a file and serializes the object into it in binary format.

            using (Stream stream = File.Open("levels.txt", FileMode.Create))
            {
                SoapFormatter formatter = new SoapFormatter();

              // BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(stream, obj);
                stream.Close();
            }
            // Empties obj.
            obj = null;

            // Opens file "levels.txt" and deserializes the object from it.
            using (Stream stream = File.Open("levels.txt", FileMode.Open))
            {
                SoapFormatter formatter = new SoapFormatter();

                //formatter = new BinaryFormatter();

                obj = (TestSimpleObject)formatter.Deserialize(stream);
                stream.Close();
            }

            Console.WriteLine("");
            Console.WriteLine("After deserialization the object contains: ");
            obj.Print();
        }
    }


    // test des objets qui sont a sérializés
    [Serializable()]
    public class TestSimpleObject
    {

        public string _enemies;
        public string _bullet;
        public string _boss;
        public string _ultraboss;



        public TestSimpleObject()
        {

            _enemies = "e";
            _bullet = "b";
            _boss = "B";
            _ultraboss = "UB";

        }


        public void Print()
        {

            Console.WriteLine("enemies = '{0}'", _enemies);
            Console.WriteLine("bullet = '{0}'", _bullet);
            Console.WriteLine("member3 = '{0}'", _boss);
            Console.WriteLine("member4 = '{0}'", _ultraboss);

        }
    }
}



