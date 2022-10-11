using System;
using System.IO;
using System.Linq;

namespace DbD_Autoskillchecks.Core
{
    internal class ReadSaveFile
    {
        static private string directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        static private string fileName = Path.Combine(directory, "Settings.txt");
        private int fileLength = File.ReadLines(fileName).Count();

        public ReadSaveFile()
        {

        }

        public int GetIntFromFile(int index)
        {
            int[] ints = new int[4];
            string[] strings = File.ReadAllLines(fileName);
            ints = Array.ConvertAll(strings, s => int.Parse(s));
            var value = ints[index];
            return value;
        }

        public int OverlapPixels
        {
            get { return GetIntFromFile(0); }
        }

        public int Tolerance
        {
            get { return GetIntFromFile(1); }
        }

        public int DelayFrame
        {
            get { return GetIntFromFile(2); }
        }
        public int MinRemainingPixels
        {
            get { return GetIntFromFile(3); }
        }
    }
}