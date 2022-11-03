using DbD_Autoskillchecks.Core.Files;
using DbD_Autoskillchecks.Core.Skillcheck;
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
		ImageSearchUtil isu = new ImageSearchUtil();

		public int Example
		{
			get
			{
				return 0;
			}
		}

		public void CoreHandlerMain()
		{
			PropertyInit();
			RunImageAlgorithm(isu.GetBitmapFromScreen());
		}

		public void DebugRoutine()
		{
			PropertyInit();
			var path = "C:\\Users\\jona4\\Desktop\\test.png";
			RunImageAlgorithm(isu.GetBitmapFromFile(path));
		}

		public void PropertyInit()
		{
			SaveFile save = new SaveFile();
			save.AddProperty("GraceZone", 5);
			save.AddProperty("SearchCircle", 600);
			save.AddProperty("SkillcheckRadius", 65);
			save.SaveToFile();
		}
		public void RunImageAlgorithm(Bitmap bmp)
		{
			ia.SkillCheckRoutine(bmp);
		}
	}
}
