using DbD_Autoskillchecks.Core.Files;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbD_Autoskillchecks.Core
{
	class CoreHandler
	{
		ImageAlgorithm ia = new ImageAlgorithm();
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
		public void RunImageAlgorithm(Bitmap bmp)
		{
			ia.StartRoutine(bmp);
		}
	}
}
