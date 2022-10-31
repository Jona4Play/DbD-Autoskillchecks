using DbD_Autoskillchecks.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DbD_Autoskillchecks.Core.Files
{
	internal class SaveFile
	{
		TargetDirectory targetdirectory = new TargetDirectory();

		public enum SaveProperties
		{
			OverlapPixels
		}
		private Dictionary<int, string> PropertyPairs;

		public void AddProperty(int s, string name)
		{
			PropertyPairs.Add(s, name);
		}
		public void RemoveProperty(string property)
		{
			var KeyToValue = PropertyPairs.FirstOrDefault(x => x.Value == property).Key;
			PropertyPairs.Remove(KeyToValue);
		}

		public void SaveToFile()
		{
			var settings = Path.Combine(targetdirectory.TargetPath, "Settings.txt");
			try
			{
				// Check if file already exists. If yes, delete it.
				if (File.Exists(settings))
				{
					File.Delete(settings);
				}
				foreach (KeyValuePair<int, string> keyValuePair in PropertyPairs)
				{
					Console.WriteLine("{0}" + "\n" + "{1}", keyValuePair.Value, keyValuePair.Key);
				}

			}
			catch (Exception Ex)
			{
				Console.WriteLine(Ex.ToString());
			}
		}
		private void ReadFromFile()
		{
			var settings = Path.Combine(targetdirectory.TargetPath, "Settings.txt");
			if (Directory.Exists(settings))
			{

			}
			else
			{
				/*
				string[] standardstrings = standardvalues.Select(x => x.ToString()).ToArray();
				File.WriteAllText(fileName, standardstrings[0] + "\n" + standardstrings[1] + "\n" + standardstrings[2] + "\n" + standardstrings[3] + "\n" + standardstrings[4]);

				return standardvalues[index];*/
			}




			//var KeyToValue = PropertyPairs.FirstOrDefault(x => x.Value == property).Key;

		}
	}
}
