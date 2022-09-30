using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbD_Autoskillchecks.Core
{
    internal class ReadSaveFile
    {
        string fileName = @"C:\Users\jona4\Desktop\ImageAlgorithm\Settings.txt";
        private int DelayFrames;
        public ReadSaveFile()
        {
            
        }

        private int GetDelayFrameFromFile()
        {
            int DelayFrame = Convert.ToInt16(File.ReadAllLines(fileName));
            return DelayFrame;
        }
        public int DelayFrame
        {
            get { return GetDelayFrameFromFile(); }
        }

    }
}
