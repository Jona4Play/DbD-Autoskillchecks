using DbD_Autoskillchecks.Core.Files;
using DbD_Autoskillchecks.Core.Keys;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace DbD_Autoskillchecks.Core
{
	class ImageAlgorithm
	{
		public int PositionRedPartX { get; private set; }
		public int PositionRedPartY { get; private set; }
		public int PositionWhiteZoneX { get; private set; }
		public int PositionWhiteZoneY { get; private set; }
		private bool FindRedSpot(double degree, Bitmap bmp)
		{
			PixelData pixelData = new PixelData();
			var scr = SaveFile.ReturnPropertyValueByName("SkillcheckRadius");
			pixelData.GetCoords(degree, scr, bmp.Size.Width / 2, bmp.Size.Height / 2);
			pixelData.GetPixelDataByCoordinate(bmp,pixelData.X, pixelData.Y);
			if (pixelData.R >= 205 && pixelData.G >= 0 && pixelData.B >= 0)
			{
				PositionRedPartX = pixelData.X;
				PositionRedPartY = pixelData.Y;
				return true;
			}
			pixelData.Dispose();
			return false;
		}
		private bool FindWhiteSpot(double degree, Bitmap bmp)
		{
			//Console.WriteLine("Calling FindWhiteSpot");
			PixelData pixelData = new PixelData();
			var w = SaveFile.ReturnPropertyValueByName("SkillcheckRadius");
			//Console.WriteLine(w);
			pixelData.GetCoords(degree, w, bmp.Size.Width / 2, bmp.Size.Height / 2);
			pixelData.GetPixelDataByCoordinate(bmp, pixelData.X, pixelData.Y);
			if (pixelData.R >= 250 && pixelData.G >= 250 && pixelData.B >= 250)
			{
				Console.WriteLine("Found White Spot at: X = {0}; Y = {1}; With These Values: R = {2}; G = {3}; B = {4}", pixelData.X, pixelData.Y, pixelData.R, pixelData.G, pixelData.B);
				PositionWhiteZoneX = pixelData.X;
				PositionWhiteZoneY = pixelData.Y;
				return true;
			}
			
			pixelData.Dispose();
			return false;
		}
		private bool RedPart(double degree, Bitmap bmp)
		{
			var sc = SaveFile.ReturnPropertyValueByName("SearchCircle");
			for (int i = 0; i < sc; i++)
			{
				if (FindRedSpot(degree, bmp))
				{
					return true;
				}
				degree += 0.5;
			}
			return false;
		}
		private bool WhitePart(double degree, Bitmap bmp)
		{
			var sc = SaveFile.ReturnPropertyValueByName("SearchCircle");
			//Console.WriteLine("Search Circle: " + sc);
			for (int i = 0; i < sc; i++)
			{
				if (FindWhiteSpot(degree, bmp))
				{
					return true;
				}
				degree += 0.5;
			}
			return false;
		}

		public void SkillCheckRoutine(Bitmap bmp)
		{
			double degree = -90;
			if (WhitePart(degree, bmp))
			{
				
				if (RedPart(degree, bmp))
				{
					var gracezone = SaveFile.ReturnPropertyValueByName("GraceZone");
					if (PositionWhiteZoneX + gracezone < PositionRedPartX
						&& PositionWhiteZoneX - gracezone > PositionRedPartX
						&& PositionWhiteZoneY + gracezone < PositionRedPartY
						&& PositionWhiteZoneY - gracezone > PositionRedPartY)
					{
						Console.WriteLine("Found Overlap" + "\n" + "Pressing Button");
						SendKeys send = new SendKeys();
						send.SendKeysToDbD(0x20);
					}
				}
			}
			bmp.Dispose();
		}
		private Bitmap DrawDebug(Bitmap bmp, double degree)
		{
			PixelData pixelData = new PixelData();
			var sc = SaveFile.ReturnPropertyValueByName("SearchCircle");
			//Console.WriteLine("Search Circle: " + sc);
			
			for (int i = 0; i < sc; i++)
			{
				pixelData.GetCoords(degree, SaveFile.ReturnPropertyValueByName("SkillcheckRadius"),bmp.Size.Width/2,bmp.Size.Height /2);
				bmp.SetPixel(pixelData.X,pixelData.Y, Color.Orange);
				degree += 0.5;
			}
			return bmp;
		}
		public void DebugStart(Bitmap bmp)
		{
			double degree = -90;
			TargetDirectory targetDirectory = new TargetDirectory();
			var path = Path.Combine(targetDirectory.TargetPath, "debugdrawn.bmp");
			var w = DrawDebug(bmp, degree);
			w.Save(path, ImageFormat.Bmp);
		}
	}
}
