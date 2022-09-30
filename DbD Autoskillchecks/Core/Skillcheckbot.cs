﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using DbD_Autoskillchecks.Core;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace DbD_Autoskillchecks.Core
{
    class Skillcheckbot : IDisposable
    {


        //parameters for Skillcheck Detection
        int tolerancePx = 7;
        int minremainingPixels = 280;
        int outerradius = 70;
        int centerx = Screen.PrimaryScreen.Bounds.Width / 2;
        int centery = Screen.PrimaryScreen.Bounds.Height / 2;
        Filter filter = new Filter();
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
        public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
        public const int VK_SPACE = 0x20; //Right Control key code

        public void SkillcheckExecute(bool SaveImage, int tolerance = 0)
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
            Console.WriteLine("Start Cyle");
            var watch = Stopwatch.StartNew();
            Bitmap screenshot = new Bitmap(outerradius * 2, outerradius * 2, PixelFormat.Format24bppRgb);
            var gfxScreenshot = Graphics.FromImage(screenshot);
            gfxScreenshot.CopyFromScreen(centerx - outerradius, centery - outerradius, 0, 0, new Size(140, 140), CopyPixelOperation.SourceCopy);
            ///Bitmap compBmp = (Bitmap)Bitmap.FromFile("C:\\Users\\jona4\\Desktop\\ImageAlgorithm\\comp.bmp");
            int WhitePixels = DetectColorWithUnsafe(screenshot, 255, 255, 255, tolerance);
            if (SaveImage) 
            {
                
                filter.DetectColorWithUnsafe(screenshot, 255, 255, 255, 0);
                screenshot.Save("C:\\Users\\jona4\\Desktop\\ImageAlgorithm\\debug.bmp", ImageFormat.Bmp);
                Console.WriteLine("Ran Debug");
            }
            ///Console.WriteLine("Program now testing Similarities");
            
            if (WhitePixels >= 200)
            {
                Console.WriteLine("Enough White Pixels");
                if(WhitePixels < minremainingPixels)
                {
                    Console.WriteLine("Overlap");
                    pressSpace();
                }
                
            }
            else
            {
                Console.WriteLine("Not enough white pixels");
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            //Console.WriteLine("Time Elapsed " + elapsedMs);
            Console.WriteLine("Stop Cycle");
        }
        private static unsafe int DetectColorWithUnsafe(Bitmap image, byte searchedR, byte searchedG, int searchedB, int tolerance)
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
            if(WhitePixelCount > 0)
            {
                Console.WriteLine("Filter ran successfully and searched for " + WhitePixelCount + " Pixels");
            }
                
            image.UnlockBits(imageData);
            return WhitePixelCount;

        }
        
        private void pressSpace(int DelayFrame = 0)
        {
            double delayMs = Math.Ceiling(DelayFrame * 16.666f);
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