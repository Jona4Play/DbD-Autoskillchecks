using System;
using System.IO;
using System.Linq;

namespace DbD_Autoskillchecks.Core
{
    internal class ReadSaveFile
    {
        private string fileName = @"C:\Users\jona4\Desktop\ImageAlgorithm\Settings.txt";
        private int fileLength = File.ReadLines(@"C:\Users\jona4\Desktop\ImageAlgorithm\Settings.txt").Count();

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