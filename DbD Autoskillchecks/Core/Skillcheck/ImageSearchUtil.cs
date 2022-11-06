using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DbD_Autoskillchecks.Core
{
	static class ImageSearchUtil
	{
		static public int[] GetCoords(double rad, int radius, int bmpHalfSize)
		{
			int x = (int)Math.Round(Math.Cos(rad) * radius + bmpHalfSize);
			int y = (int)Math.Round(Math.Sin(rad) * radius + bmpHalfSize);
			int[] ints = new int[] { x, y };
			return ints;
		}

		static public unsafe int DetectColorWithUnsafe(Bitmap image, byte searchedR, byte searchedG, int searchedB, int tolerance)
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

		static public unsafe void DetectColorWithUnsafeImage(Bitmap image, byte searchedR, byte searchedG, int searchedB, int tolerance)
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

		static public Bitmap GetBitmapFromScreen()
		{
			int screendimX = 1920;
			int screendimY = 1080;
			int bitmapdimension = 140;
			Bitmap screenshot = new Bitmap(bitmapdimension, bitmapdimension, PixelFormat.Format24bppRgb);
			var gfxScreenshot = Graphics.FromImage(screenshot);
			gfxScreenshot.CopyFromScreen(screendimX / 2 - bitmapdimension / 2, screendimY / 2 - bitmapdimension / 2, 0, 0, new Size(bitmapdimension, bitmapdimension), CopyPixelOperation.SourceCopy);
			return screenshot;
		}

		static public Bitmap GetBitmapFromFile(string path)
		{
			var bmp = new Bitmap(path);
			return bmp;
		}

		static public void GetCroppedImageFromFile(string path)
		{
			TargetDirectory targetDirectory = new TargetDirectory();
			var pathbmp = Path.Combine(targetDirectory.TargetPath, "debugcropped.bmp");
			var bmp = Crop(path, 140, 140, 1920 / 2 - 70, 1080 / 2 - 70);
		}
		static	public Bitmap Crop(string img, int width, int height, int x, int y)
		{
			try
			{
				Image image = Image.FromFile(img);
				Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
				bmp.SetResolution(80, 60);

				Graphics gfx = Graphics.FromImage(bmp);
				gfx.SmoothingMode = SmoothingMode.AntiAlias;
				gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
				gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
				gfx.DrawImage(image, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
				// Dispose to free up resources
				image.Dispose();
				gfx.Dispose();

				return bmp;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
		}
	}
}