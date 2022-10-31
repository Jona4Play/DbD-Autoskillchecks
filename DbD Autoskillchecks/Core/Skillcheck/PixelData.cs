﻿using System;
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

		public void GetCoords(Bitmap bitmap, double degree, double radius, int halfBitMap)
		{
			X = (int)Math.Round(Math.Cos(degree) * radius + halfBitMap);
			Y = (int)Math.Round(Math.Cos(degree) * radius + halfBitMap);
		}

	}
}