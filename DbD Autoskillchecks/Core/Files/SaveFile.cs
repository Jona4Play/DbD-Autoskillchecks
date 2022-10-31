using DbD_Autoskillchecks.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace DbD_Autoskillchecks.Core.Files
{
	enum SaveProperties
	{
		OverlapPixels,

	}

	internal class Property
	{

		public int PropertyAmount
		{
			get
			{
				return typeof(Property).GetProperties().Count() - 1;
			}
		}

		public int ID { get; set; }

		public string PropertyName { get; set; }
		public int Value { get; set; }

		public Property()
		{

		}
		public Property(int iD, string propertyName, int value)
		{
			ID = iD;
			PropertyName = propertyName;
			Value = value;
		}
	}
	internal class SaveFile
	{
		public int ElementListCount
		{
			get
			{
				int counter = 0;
				var settings = Path.Combine(targetdirectory.TargetPath, "Settings.txt");
				using (StreamReader r = new StreamReader(settings))
				{
					int i = 0;
					while (r.ReadLine() != null) { i++; counter++; }
				}

				return counter / 3;
			}
		}

		TargetDirectory targetdirectory = new TargetDirectory();


		List<Property> Properties = new List<Property>
		{

		};


		public void AddProperty(string name, int value)
		{
			var findValueByName = (from prop in Properties
								   where prop.PropertyName == name
								   select new Property { ID = prop.ID, PropertyName = prop.PropertyName, Value = value }
								   );

			if (findValueByName != null)
			{
				Properties.Add(new Property(Properties.Count + 1, name, value));
			}
			else
			{
				Console.WriteLine("A Property is already registered under this name");
			}
		}
		public void RemovePropertyByName(string name)
		{
			var findPropByName = (from prop in Properties
								  where prop.PropertyName == name
								  select prop);
			Properties.Remove((Property)findPropByName);
		}
		public void RemovePropertyByID(int id)
		{
			var findPropByID = (from prop in Properties
								where prop.ID == id
								select prop);
			Properties.Remove((Property)findPropByID);
		}

		public int ReturnPropertyValueByName(string name)
		{
			foreach (var property in Properties)
			{
				if (property.PropertyName == name)
				{
					return property.Value;
				}
			}
			return 0;
		}
		public int ReturnPropertyValueByID(int id)
		{
			foreach (var property in Properties)
			{
				if (property.ID == id)
				{
					return property.Value;
				}
			}
			return 0;
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

				using (var sw = new System.IO.StreamWriter(settings))
				{
					foreach (var proper in Properties)
					{
						//Console.WriteLine("{0}" + "\n" + "{1}" + "\n" + "{2}", proper.ID, proper.PropertyName, proper.Value);
						sw.WriteLine(proper.ID);
						sw.WriteLine(proper.PropertyName);
						sw.WriteLine(proper.Value);
					}
				}
			}
			catch (Exception Ex)
			{
				Console.WriteLine(Ex.ToString());
			}
		}
		public void ReadFromFile()
		{
			//Console.WriteLine("Reading File");
			var settings = Path.Combine(targetdirectory.TargetPath, "Settings.txt");
			Property property = new Property();

			if (File.Exists(settings))
			{
				//Console.WriteLine("File Found");
				int thirdline = 0;
				string secondline = "";
				string[] tempstrings = new string[ElementListCount];
				int[] tempints = new int[ElementListCount];
				using (StreamReader r = new StreamReader(settings))
				{
					for (int c = 0; c < ElementListCount; c++)
					{
						for (int v = 0; v < property.PropertyAmount; v++)
						{
							Console.WriteLine(v);
							switch (v)
							{
								case 0:
									r.ReadLine();
									break;
								case 1:
									secondline = r.ReadLine();
									break;
								case 2:
									thirdline = int.Parse(r.ReadLine());
									break;
							}

						}
						//Console.WriteLine(c);
						//Console.WriteLine(secondline);
						//Console.WriteLine(thirdline);
						tempstrings[c] = secondline;
						tempints[c] = thirdline;

					}
					Properties.Clear();
					for (int i = 0; i < ElementListCount; i++)
					{
						AddProperty(tempstrings[i], tempints[i]);
					}
				}
				//Console.WriteLine("Current List: ");
				foreach (Property prop in Properties)
				{
					Console.WriteLine("{0}" + "\n" + "{1}" + "\n" + "{2}" + "\n", prop.ID, prop.PropertyName, prop.Value);
				}
			}
			else
			{
				Console.WriteLine("File was not found at {0}", settings);
			}
		}
	}
}