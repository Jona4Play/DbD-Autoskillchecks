using KGySoft.Drawing.Imaging;
using KGySoft.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using KGySoft.CoreLibraries;
using System.Data;

namespace DbD_Autoskillchecks.Core.Skillcheck
{
	class PixelData
	{
		public int X { get; private set; }
		public int Y { get; private set; }
		public int R { get; private set; }
		public int G { get; private set; }
		public int B { get; private set; }



		public void GetCoords(double degree, double radius, int dimensionXhalf, int dimensionYhalf)
		{
			Console.WriteLine("Called Get Coords");
			double rad = DegreeToRadian(degree);
			X = (int)Math.Round(Math.Cos(rad) * radius + dimensionXhalf);
			Y = (int)Math.Round(Math.Sin(rad) * radius + dimensionYhalf);
			Console.WriteLine("Coords at {0} degrees: X = {1} Y = {2}",degree,X,Y);
		}

		public void GetPixelDataByCoordinate(Bitmap bmp, int x, int y)
		{
			Console.WriteLine("Called Get PixelData");
			var color = bmp.GetPixel(x, y);
			R = color.R;
			G = color.G;
			B = color.B;
		}
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
		private double DegreeToRadian(double degree)
		{
			return degree * Math.PI / 180;
		}
	}
	public class DirectBitmap : IDisposable
	{
		public Bitmap Bitmap { get; private set; }
		public Int32[] Bits { get; private set; }
		public bool Disposed { get; private set; }
		public int Height { get; private set; }
		public int Width { get; private set; }

		protected GCHandle BitsHandle { get; private set; }

		public DirectBitmap(int width, int height)
		{
			Width = width;
			Height = height;
			Bits = new Int32[width * height];
			BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
			Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format24bppRgb, BitsHandle.AddrOfPinnedObject());
		}

		public void SetPixel(int x, int y, Color colour)
		{
			int index = x + (y * Width);
			int col = colour.ToArgb();

			Bits[index] = col;
		}

		public Color GetPixel(int x, int y)
		{
			int index = x + (y * Width);
			int col = Bits[index];
			Color result = Color.FromArgb(col);

			return result;
		}

		public void Dispose()
		{
			if (Disposed) return;
			Disposed = true;
			Bitmap.Dispose();
			BitsHandle.Free();
		}

	}
}
