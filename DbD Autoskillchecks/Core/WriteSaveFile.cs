using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbD_Autoskillchecks.Core
{
    internal class WriteSaveFile
    {
        public void SaveToFile()
        {
            string currentWorkingDirectory = Directory.GetCurrentDirectory();
            File.WriteAllText(currentWorkingDirectory + "Settings.txt","Settings");
        }
    }
}
