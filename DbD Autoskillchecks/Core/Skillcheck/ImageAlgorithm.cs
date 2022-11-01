using DbD_Autoskillchecks.Core.Keys;
using DbD_Autoskillchecks.Core.Skillcheck;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbD_Autoskillchecks.Core
{
	class ImageAlgorithm
	{
		PixelData pixelData = new PixelData();

		public int PositionRedPartX { get; private set; }
		public int PositionRedPartY { get; private set; }
		public int PositionWhiteZoneX { get; private set; }
		public int PositionWhiteZoneY { get; private set; }
		private bool FindRedSpot(double degree, Bitmap bmp)
		{
			pixelData.GetCoords(degree, 65, bmp.Size.Width / 2);
			if (pixelData.R >= 205 && pixelData.G >= 0 && pixelData.B >= 0)
			{
				return true;
			}
			return false;
		}
		private bool FindWhiteSpot(double degree, Bitmap bmp)
		{
			pixelData.GetCoords(degree, 65, bmp.Size.Width / 2);
			if (pixelData.R >= 250 && pixelData.G >= 250 && pixelData.B >= 250)
			{
				return true;
			}
			return false;
		}
		private bool RedPart(double degree, Bitmap bmp)
		{
			for (int i = 0; i < 600; i++)
			{
				if (FindRedSpot(degree, bmp))
				{
					return true;
				}
				degree -= 0.5;
			}
			return false;
		}
		private bool WhitePart(double degree, Bitmap bmp)
		{
			for (int i = 0; i < 600; i++)
			{
				if (FindWhiteSpot(degree, bmp))
				{
					return true;
				}
				degree -= 0.5;
			}
			return false;
		}

		public void StartRoutine(Bitmap bmp)
		{
			double degree = 0;
			if (WhitePart(degree, bmp))
			{
				if (RedPart(degree, bmp))
				{
					Console.WriteLine("Found Overlap" + "\n" + "Pressing Button");
					SendKeys send = new SendKeys();
					send.SendKeysToDbD(0x20);
				}
			}
		}
	}
}
