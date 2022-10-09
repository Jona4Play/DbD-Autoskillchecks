using System;
using System.IO;

namespace DbD_Autoskillchecks.Core
{
    internal class WriteSaveFile
    {
        public void SaveToFile(int overlappixels, int tolerance, int delayframes, int minremainingpixels)
        {
            Console.WriteLine("Called SaveToFile");
            string fileName = @"C:\Users\jona4\Desktop\ImageAlgorithm\Settings.txt";

            try
            {
                // Check if file already exists. If yes, delete it.
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                File.WriteAllText(fileName, overlappixels + "\n" + tolerance + "\n" + delayframes + "\n" + minremainingpixels +"\n");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}