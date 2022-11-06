using DbD_Autoskillchecks.Core.Files;
using DbD_Autoskillchecks.Core.Skillcheck;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DbD_Autoskillchecks.Core
{
	class CoreHandler
	{
		TargetDirectory targetDirectory = new TargetDirectory();
		public void CoreHandlerMain()
		{
			CoreHandlerGlobal.PropertyInit();
			
		}

		
		
		
	}
	static class CoreHandlerGlobal
	{
		static public void DebugRoutine()
		{
			var watch = Stopwatch.StartNew();
			PropertyInit();
			//RunImageAlgorithm(ImageSearchUtil.GetBitmapFromScreen());
			//RunImageAlgorithm(ImageSearchUtil.GetBitmapFromScreen());
			RunIADebug(ImageSearchUtil.GetBitmapFromScreen());
			watch.Stop();

		}
		public static void RunImageAlgorithm(Bitmap bmp)
		{
			ImageAlgorithm ia = new ImageAlgorithm();
			ia.SkillCheckRoutine(bmp);
		}
		private static void RunIADebug(Bitmap bmp)
		{
			ImageAlgorithm ia = new ImageAlgorithm();
			ia.DebugStart(bmp);
		}
		static public void PropertyInit()
		{
			SaveFile.AddProperty("GraceZone", 5);
			SaveFile.AddProperty("SearchCircle", 600);
			SaveFile.AddProperty("SkillcheckRadius", 65);
			SaveFile.SaveToFile();
		}
		static public IntPtr GetGameID()
		{
			var processID = 0;
			Process[] process = Process.GetProcessesByName("DeadByDaylight-Win64-Shipping");
			if (process.Length == 1)
			{
				foreach (var proc in process)
				{
					processID = proc.Id;
				}
			}
			IntPtr MainHandle = Process.GetProcessById(processID).MainWindowHandle;
			return MainHandle;

		}
	}
}
