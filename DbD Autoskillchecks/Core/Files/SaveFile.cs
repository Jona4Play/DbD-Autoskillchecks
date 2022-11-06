using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using Path = System.IO.Path;

namespace DbD_Autoskillchecks.Core.Files
{
	class Property
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

	static class SaveFile
	{
		private static int ElementListCount = 0;
		


		private static TargetDirectory targetdirectory = new TargetDirectory();

		private static List<Property> Properties = new List<Property>
		{
		};
		public static void GetElementCount()
		{
			ElementListCount = typeof(Property).GetProperties().Count();
		}
		public static void AddProperty(string name, int value)
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

		public static void RemovePropertyByName(string name)
		{
			var findPropByName = (from prop in Properties
								  where prop.PropertyName == name
								  select prop);
			Properties.Remove((Property)findPropByName);
		}

		public static void RemovePropertyByID(int id)
		{
			var findPropByID = (from prop in Properties
								where prop.ID == id
								select prop);
			Properties.Remove((Property)findPropByID);
		}

		public static int ReturnPropertyValueByName(string name)
		{
			foreach (var property in Properties)
			{
				//Console.WriteLine("Called for Name comparison");
				if (property.PropertyName.ToLower() == name.ToLower())
				{
					//Console.WriteLine("Found Match");
					return property.Value;
				}
			}
			return 0;
		}

		public static int ReturnPropertyValueByID(int id)
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

		public static void SaveToFile()
		{
			var settings = Path.Combine(targetdirectory.TargetPath, "Settings.txt");
			try
			{
				//Console.WriteLine(settings);
				// Check if file already exists. If yes, delete it.
				if (File.Exists(settings))
				{
					File.Delete(settings);
				}

				using (var sw = new System.IO.StreamWriter(settings))
				{
					foreach (var proper in Properties)
					{

						Console.WriteLine("{0}" + "\n" + "{1}" + "\n" + "{2}", proper.PropertyName, proper.ID, proper.Value);
						sw.WriteLine(proper.PropertyName);
						sw.WriteLine(proper.ID);
						sw.WriteLine(proper.Value);
					}
				}
			}
			catch (Exception Ex)
			{
				Console.WriteLine(Ex.ToString());
			}
		}

		public static void ReadFromFile()
		{
			Console.WriteLine("Reading File");
			var settings = Path.Combine(targetdirectory.TargetPath, "Settings.txt");
			Property property = new Property();

			Properties.Clear();
			
			if (File.Exists(settings))
			{
				Console.WriteLine("File Found Settings: " + settings);
				int propvalue = 0;
				string propname = "";
				string[] tempstrings = new string[ElementListCount];
				int[] tempints = new int[ElementListCount];
				using (StreamReader r = new StreamReader(settings))
				{
					for (int c = 0; c < ElementListCount; c++)
					{
						for (int v = 0; v < property.PropertyAmount; v++)
						{
							//Console.WriteLine(v);
							switch (v)
							{
								case 0:
									propname = r.ReadLine();
									break;

								case 1:
									r.ReadLine();
									break;

								case 2:
									propvalue = int.Parse(r.ReadLine());
									break;
							}
						}
						//Console.WriteLine(c);
						//Console.WriteLine(secondline);
						//Console.WriteLine(thirdline);
						tempstrings[c] = propname;
						tempints[c] = propvalue;
					}

					for (int i = 0; i < ElementListCount; i++)
					{
						Console.WriteLine("{0} \n {1} \n {2} \n",tempstrings[i], i, tempints[i]);
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