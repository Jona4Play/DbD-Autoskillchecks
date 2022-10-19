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

		private int[] standardvalues = new int[] { 280, 0, 0, 200, 130 };

		public int GetIntFromFile(int index)
		{
			string fileName = Path.Combine(targetDirectory.TargetPath, "Settings.txt");
			int[] ints = new int[5];
			if (Directory.Exists(fileName))
			{
				string[] strings = File.ReadAllLines(fileName);
				ints = Array.ConvertAll(strings, s => int.Parse(s));
				var value = ints[index];
				return value;
			}
			else
			{
				string[] standardstrings = standardvalues.Select(x => x.ToString()).ToArray();
				File.WriteAllText(fileName, standardstrings[0] + "\n" + standardstrings[1] + "\n" + standardstrings[2] + "\n" + standardstrings[3] + "\n" + standardstrings[4]);

				return standardvalues[index];
			}
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