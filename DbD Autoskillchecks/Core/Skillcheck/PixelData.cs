using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbD_Autoskillchecks.Core.Skillcheck
{
	class PixelData
	{
		public int X { get; private set; }
		public int Y { get; private set; }
		public int R { get; private set; }
		public int G { get; private set; }
		public int B { get; private set; }



		public void GetCoords(double degree, double radius, int halfBitMap)
		{
			double rad = DegreeToRadian(degree);
			X = (int)Math.Round(Math.Cos(rad) * radius + halfBitMap);
			Y = (int)Math.Round(Math.Cos(rad) * radius + halfBitMap);
		}

		public void GetPixelData(Bitmap bmp)
		{
			var color = bmp.GetPixel(X, Y);
			R = color.R;
			G = color.G;
			B = color.B;
		}

		private double DegreeToRadian(double degree)
		{
			return degree * Math.PI / 180;
		}

	}
}
