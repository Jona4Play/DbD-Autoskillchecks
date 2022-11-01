using DbD_Autoskillchecks.Core.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbD_Autoskillchecks.Core
{
	class CoreHandler
	{
		public int Example
		{
			get
			{
				return 0;
			}
		}

		public void CoreHandlerMain()
		{

		}

		public void PropertyInit()
		{
			SaveFile save = new SaveFile();
			save.AddProperty("Overlap", 200);
		}
	}
}
