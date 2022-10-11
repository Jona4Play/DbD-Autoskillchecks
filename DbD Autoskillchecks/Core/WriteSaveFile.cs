using System;
using System.IO;

namespace DbD_Autoskillchecks.Core
{
    internal class WriteSaveFile
    {
        public void SaveToFile(int overlappixels, int tolerance, int delayframes, int minremainingpixels)
        {
            string directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string settings = Path.Combine(directory, "Settings.txt");
            Console.WriteLine("Called SaveToFile and saving to {0}", settings);
            string fileName = settings;

            try
            {
                // Check if file already exists. If yes, delete it.
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                File.WriteAllText(fileName, overlappixels + "\n" + tolerance + "\n" + delayframes + "\n" + minremainingpixels + "\n");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}