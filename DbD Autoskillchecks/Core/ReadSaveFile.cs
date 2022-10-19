using System;
using System.IO;
using System.Linq;

namespace DbD_Autoskillchecks.Core
{
	internal class ReadSaveFile
	{
		private TargetDirectory targetDirectory = new TargetDirectory();

		public ReadSaveFile()
		{

		}

		public int GetIntFromFile(int index)
		{
			string fileName = Path.Combine(targetDirectory.TargetPath, "Settings.txt");
			int[] ints = new int[5];
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
		public int MoonwalkDelay
		{
			get { return GetIntFromFile(4); }
		}
	}
}