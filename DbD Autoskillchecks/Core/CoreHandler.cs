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
	
	static class CoreHandler
	{
		static public void DebugRoutine()
		{
			TargetDirectory target = new TargetDirectory();
			var watch = Stopwatch.StartNew();
			//PropertyInit();
			//var path = Path.Combine(target.TargetPath,"testreal.png");
			//var destpath = Path.Combine(target.TargetPath,"cropreal.png");
			//var img = ImageSearchUtil.Crop(path, 140, 140, 1920 / 2 - 70, 1080 / 2 - 70);
			//img.Save(destpath,ImageFormat.Bmp);
			RunImageAlgorithm(ImageSearchUtil.GetBitmapFromScreen());
			//RunImageAlgorithm(ImageSearchUtil.GetBitmapFromScreen());
			//RunIADebug(ImageSearchUtil.GetBitmapFromScreen());
			watch.Stop();
			Console.WriteLine(watch.ElapsedMilliseconds);

		}
		public static void RunImageAlgorithm(Bitmap bmp, bool debug = false)
		{
			Console.WriteLine("Run IA");
			ImageAlgorithm ia = new ImageAlgorithm();
			ia.SkillCheckRoutine(bmp, debug);
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
			SaveFile.AddProperty("DelayInMS", 12);
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
