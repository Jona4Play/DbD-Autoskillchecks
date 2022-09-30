using System;
using System.IO;

namespace DbD_Autoskillchecks.Core
{
    internal class WriteSaveFile
    {
        public void SaveToFile(int remainingpixels, int tolerance, int delayframes)
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

                File.WriteAllText(fileName, remainingpixels + "\n" + tolerance + "\n" + delayframes + "\n");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}