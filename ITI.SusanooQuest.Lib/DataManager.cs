using System;
using System.IO;
using System.Collections.Generic;

namespace ITI.SusanooQuest.Lib
{
    public static class DataManager
    {
        static readonly string _path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/SusanooQuest";
        static readonly string _fileName = "data.sq";

        public static void Writer(ushort maxLive, uint highScore)
        {
            if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);

            using (BinaryWriter writer = new BinaryWriter(File.Open($"{_path}/{_fileName}", FileMode.Create)))
            {
                writer.Write(maxLive);
                writer.Write(highScore);
            }
        }

        public static Tuple<ushort, uint> Reader()
        {
            ushort maxLive;
            uint highScore;
            if (File.Exists($"{_path}/{_fileName}"))
            {
                using (BinaryReader reader = new BinaryReader(File.Open($"{_path}/{_fileName}", FileMode.Open)))
                {
                    maxLive = reader.ReadUInt16();
                    highScore = reader.ReadUInt32();
                }
            }
            else
            {
                maxLive = 3;
                highScore = 0;
            }

            return new Tuple<ushort, uint>(maxLive, highScore);
        }
    }
}
