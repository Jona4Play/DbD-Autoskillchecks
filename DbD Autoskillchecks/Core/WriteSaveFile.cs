using System;
using System.IO;

namespace DbD_Autoskillchecks.Core
{
	internal class WriteSaveFile
	{
		TargetDirectory targetDirectory = new TargetDirectory();
		public void SaveToFile(int overlappixels, int tolerance, int delayframes, int minremainingpixels, int moonwalkDelay)
		{
			string settings = Path.Combine(targetDirectory.TargetPath, "Settings.txt");
			Console.WriteLine("Called SaveToFile and saving to {0}", settings);

			try
			{
				// Check if file already exists. If yes, delete it.
				if (File.Exists(settings))
				{
					File.Delete(settings);
				}

				File.WriteAllText(settings, overlappixels + "\n" + tolerance + "\n" + delayframes + "\n" + minremainingpixels + "\n" + moonwalkDelay + "\n");
			}
			catch (Exception Ex)
			{
				Console.WriteLine(Ex.ToString());
			}
		}
	}
}