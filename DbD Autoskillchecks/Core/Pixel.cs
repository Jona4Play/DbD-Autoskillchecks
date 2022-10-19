using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbD_Autoskillchecks.Core
{
	internal class Pixel
	{
		private int LastFrameWhitePixel;

		public int LastFrameWhitePixels
		{
			get { return LastFrameWhitePixel; }
			set { LastFrameWhitePixel = value; }
		}

		public int[] GetCoords(double rad, int radius, int bmpHalfSize)
		{
			int x = (int)Math.Round(Math.Cos(rad) * radius + bmpHalfSize);
			int y = (int)Math.Round(Math.Sin(rad) * radius + bmpHalfSize);
			int[] ints = new int[] { x, y };
			return ints;
		}

		public unsafe int DetectColorWithUnsafe(Bitmap image, byte searchedR, byte searchedG, int searchedB, int tolerance)
		{
			BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			int bytesPerPixel = 3;

			byte* scan0 = (byte*)imageData.Scan0.ToPointer();
			int stride = imageData.Stride;
			int WhitePixelCount = 0;
			byte unmatchingValue = 0;
			byte matchingValue = 255;
			int toleranceSquared = tolerance * tolerance;

			for (int y = 0; y < imageData.Height; y++)
			{
				byte* row = scan0 + (y * stride);

				for (int x = 0; x < imageData.Width; x++)
				{
					// Watch out for actual order (BGR)!
					int bIndex = x * bytesPerPixel;
					int gIndex = bIndex + 1;
					int rIndex = bIndex + 2;

					byte pixelR = row[rIndex];
					byte pixelG = row[gIndex];
					byte pixelB = row[bIndex];

					int diffR = pixelR - searchedR;
					int diffG = pixelG - searchedG;
					int diffB = pixelB - searchedB;

					int distance = diffR * diffR + diffG * diffG + diffB * diffB;

					if (distance > toleranceSquared)
					{
						row[rIndex] = row[bIndex] = row[gIndex] = unmatchingValue;
					}
					else
					{
						row[rIndex] = row[bIndex] = row[gIndex] = matchingValue;
						WhitePixelCount += 1;
					}
				}
			}
			if (WhitePixelCount > 0)
			{
				Console.WriteLine("Filter ran successfully and searched for " + WhitePixelCount + " Pixels");
			}

			image.UnlockBits(imageData);
			return WhitePixelCount;
		}
		public unsafe void DetectColorWithUnsafeImage(Bitmap image, byte searchedR, byte searchedG, int searchedB, int tolerance)
		{
			BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width,
			  image.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			int bytesPerPixel = 3;

			byte* scan0 = (byte*)imageData.Scan0.ToPointer();
			int stride = imageData.Stride;

			byte unmatchingValue = 0;
			byte matchingValue = 255;
			int toleranceSquared = tolerance * tolerance;

			for (int y = 0; y < imageData.Height; y++)
			{
				byte* row = scan0 + (y * stride);

				for (int x = 0; x < imageData.Width; x++)
				{
					// Watch out for actual order (BGR)!
					int bIndex = x * bytesPerPixel;
					int gIndex = bIndex + 1;
					int rIndex = bIndex + 2;

					byte pixelR = row[rIndex];
					byte pixelG = row[gIndex];
					byte pixelB = row[bIndex];

					int diffR = pixelR - searchedR;
					int diffG = pixelG - searchedG;
					int diffB = pixelB - searchedB;

					int distance = diffR * diffR + diffG * diffG + diffB * diffB;

					row[rIndex] = row[bIndex] = row[gIndex] = distance >
					  toleranceSquared ? unmatchingValue : matchingValue;
				}
			}

			image.UnlockBits(imageData);
		}

	}
}
