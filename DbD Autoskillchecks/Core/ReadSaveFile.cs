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

        public void GetIntFromFile()
        {
            int[] ints = new int[3];
            string[] strings = File.ReadAllLines(fileName);
            ints = Array.ConvertAll(strings, s => int.Parse(s));
            foreach (int i in ints)
                Console.WriteLine(i);
        }

        private int GetDelayFrameFromFile()
        {
            int DelayFrame = Convert.ToInt32(File.ReadAllLines(fileName));
            return DelayFrame;
        }

        public int DelayFrame
        {
            get { return GetDelayFrameFromFile(); }
        }

        public int MinRemainingPixels
        {
            get { return GetDelayFrameFromFile(); }
        }

        public int Tolerance
        {
            get { return GetDelayFrameFromFile(); }
        }
    }
}