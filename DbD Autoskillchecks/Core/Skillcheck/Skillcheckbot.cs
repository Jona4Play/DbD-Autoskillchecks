using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace DbD_Autoskillchecks.Core
{
	internal class Skillcheckbot : IDisposable
	{
		private ReadSaveFile rsv = new ReadSaveFile();
		//parameters for Skillcheck Detection

		private int outerradius = 70;
		private int centerx = Screen.PrimaryScreen.Bounds.Width / 2;
		private int centery = Screen.PrimaryScreen.Bounds.Height / 2;
		private Pixel pixel = new Pixel();
		private TargetDirectory targetdir = new TargetDirectory();

		[DllImport("user32.dll", SetLastError = true)]
		private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

		public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
		public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
		public const int VK_SPACE = 0x20; //Space Key Code
		public void SkillcheckExecute(bool SaveImage)
		{
			/* Deprecated
            Console.WriteLine("Debug Version");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Bitmap screenshot = new Bitmap(outerradius * 2, outerradius * 2, PixelFormat.Format24bppRgb);
            var gfxScreenshot = Graphics.FromImage(screenshot);
            gfxScreenshot.CopyFromScreen(centerx - outerradius, centery - outerradius, 0, 0, new Size(140, 140), CopyPixelOperation.SourceCopy);
            //Bitmap compBmp = (Bitmap)Bitmap.FromFile("C:\\Users\\jona4\\Desktop\\ImageAlgorithm\\comp.bmp");
            DetectColorWithUnsafe(screenshot, 255, 255, 255, 0);
            screenshot.Save("C:\\Users\\jona4\\Desktop\\ImageAlgorithm\\debug.bmp", ImageFormat.Bmp);
            Console.WriteLine("Program now testing Similarities");

            if (CompareMemCmp(compBmp, screenshot))
            {
                pressSpace();
                Console.WriteLine("Matching");
            }
            else
            {
                Console.WriteLine("NotMatching");
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Time Elapsed " + elapsedMs);

            Debug Section used for Development
            screenshot.Save("C:\\Users\\jona4\\Desktop\\ImageAlgorithm\\debug.bmp", ImageFormat.Bmp);
            */
			//Console.WriteLine("Start Cyle");

			var watch = Stopwatch.StartNew();

			Bitmap screenshot = new Bitmap(outerradius * 2, outerradius * 2, PixelFormat.Format24bppRgb);
			var gfxScreenshot = Graphics.FromImage(screenshot);
			gfxScreenshot.CopyFromScreen(centerx - outerradius, centery - outerradius, 0, 0, new Size(140, 140), CopyPixelOperation.SourceCopy);
			int WhitePixels = pixel.DetectColorWithUnsafe(screenshot, 255, 255, 255, rsv.Tolerance);
			if (SaveImage)
			{
				string directory = targetdir.TargetPath;
				string debugimage = Path.Combine(directory, "debug.bmp");
				string debugscaledimage = Path.Combine(directory, "debugscaled.bmp");
				pixel.DetectColorWithUnsafeImage(screenshot, 255, 255, 255, 0);

				float width = 280;
				float height = 280;
				var brush = new SolidBrush(Color.Black);

				var image = new Bitmap(screenshot);
				float scale = Math.Min(width / image.Width, height / image.Height);
				var bmp = new Bitmap((int)width, (int)height);
				var graph = Graphics.FromImage(bmp);

				// uncomment for higher quality output
				//graph.InterpolationMode = InterpolationMode.High;
				//graph.CompositingQuality = CompositingQuality.HighQuality;
				//graph.SmoothingMode = SmoothingMode.AntiAlias;

				var scaleWidth = (int)(image.Width * scale);
				var scaleHeight = (int)(image.Height * scale);

				graph.FillRectangle(brush, new RectangleF(0, 0, width, height));
				graph.DrawImage(image, ((int)width - scaleWidth) / 2, ((int)height - scaleHeight) / 2, scaleWidth, scaleHeight);
				screenshot.Save(debugimage, ImageFormat.Bmp);
				bmp.Save(debugscaledimage, ImageFormat.Bmp);
				Console.WriteLine("Ran Debug");
			}
			//Console.WriteLine("Program now testing Similarities");

			if (WhitePixels >= rsv.MinRemainingPixels)
			{
				//Console.WriteLine("Enough White Pixels");
				if ((WhitePixels - pixel.LastFrameWhitePixels) > 25 && pixel.LastFrameWhitePixels != 0)
				{
					Console.WriteLine("Overlap");
					pressSpace();
				}
			}
			else
			{
				//Console.WriteLine("Not enough white pixels");
			}
			watch.Stop();
			var elapsedMs = watch.ElapsedMilliseconds;
			//Console.WriteLine("Time Elapsed " + elapsedMs);
			//Console.WriteLine("Stop Cycle");
			pixel.LastFrameWhitePixels = WhitePixels;
		}

		private void pressSpace()
		{
			double delayMs = Math.Ceiling(rsv.DelayFrame * 16.666f);
			Task.Delay((int)delayMs);
			keybd_event(VK_SPACE, 0, KEYEVENTF_EXTENDEDKEY, 0);
			Task.Delay(2);
			keybd_event(VK_SPACE, 0, KEYEVENTF_KEYUP, 0);
			Console.WriteLine("Pressed Space");
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}