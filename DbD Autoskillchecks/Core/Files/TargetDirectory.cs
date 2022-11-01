using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbD_Autoskillchecks.Core
{
	internal class TargetDirectory
	{
		private string targetdirectory;

		public string TargetPath
		{
			get
			{
				return GetDataDirectory();
			}
		}
		public string StaticPath
		{
			get
			{
				return GetStaticDirectory();
			}
		}
		public string GetStaticDirectory()
		{
			string basedirectory = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
			//Console.WriteLine(basedirectory);
			targetdirectory = Path.Combine(basedirectory, "Static");
			CheckPathValidity(targetdirectory);
			//Console.WriteLine(targetdirectory);
			return targetdirectory;
		}
		public string GetDataDirectory()
		{
			string basedirectory = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
			//Console.WriteLine(basedirectory);
			targetdirectory = Path.Combine(basedirectory, "Data");
			CheckPathValidity(targetdirectory);
			//Console.WriteLine(targetdirectory);
			return targetdirectory;
		}

		private void CheckPathValidity(string directory)
		{
			if (Directory.Exists(directory))
			{
				//Console.WriteLine("Folder Exists");
			}
			else
			{
				Console.WriteLine("Folder doesn't exist \n Creating...");
				Directory.CreateDirectory(directory);
				Console.WriteLine("Created folder at {0}", directory);
			}
		}
	}
}
