using DbD_Autoskillchecks.Core.Files;
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
		SaveFile saveFile = new SaveFile();
		public int PositionRedPartX { get; private set; }
		public int PositionRedPartY { get; private set; }
		public int PositionWhiteZoneX { get; private set; }
		public int PositionWhiteZoneY { get; private set; }
		private bool FindRedSpot(double degree, Bitmap bmp)
		{
			pixelData.GetCoords(degree, saveFile.ReturnPropertyValueByName("SkillcheckRadius"), bmp.Size.Width / 2, bmp.Size.Height / 2);
			if (pixelData.R >= 205 && pixelData.G >= 0 && pixelData.B >= 0)
			{
				PositionRedPartX = pixelData.X;
				PositionRedPartY = pixelData.Y;
				return true;
			}
			return false;
		}
		private bool FindWhiteSpot(double degree, Bitmap bmp)
		{
			pixelData.GetCoords(degree, saveFile.ReturnPropertyValueByName("SkillcheckRadius"), bmp.Size.Width / 2, bmp.Size.Height / 2);
			if (pixelData.R >= 250 && pixelData.G >= 250 && pixelData.B >= 250)
			{
				PositionWhiteZoneX = pixelData.X;
				PositionWhiteZoneY = pixelData.Y;
				return true;
			}
			return false;
		}
		private bool RedPart(double degree, Bitmap bmp)
		{
			for (int i = 0; i < saveFile.ReturnPropertyValueByName("SearchCircle"); i++)
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
			for (int i = 0; i < saveFile.ReturnPropertyValueByName("SearchCircle"); i++)
			{
				if (FindWhiteSpot(degree, bmp))
				{
					return true;
				}
				degree -= 0.5;
			}
			return false;
		}

		public void SkillCheckRoutine(Bitmap bmp)
		{
			double degree = 90;
			if (WhitePart(degree, bmp))
			{
				if (RedPart(degree, bmp))
				{
					var gracezone = saveFile.ReturnPropertyValueByName("GraceZone");
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
	}
}
