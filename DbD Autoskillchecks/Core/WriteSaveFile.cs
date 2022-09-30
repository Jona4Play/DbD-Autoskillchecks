using System;
using System.IO;

namespace DbD_Autoskillchecks.Core
{
    internal class WriteSaveFile
    {
        public void SaveToFile()
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

                File.WriteAllText(fileName, "{0}\n" + "{1}\n" + "{2}\n");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}