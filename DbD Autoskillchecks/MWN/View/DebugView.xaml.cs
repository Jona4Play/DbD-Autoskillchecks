using DbD_Autoskillchecks.Core;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Cache;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Image = System.Drawing.Image;

namespace DbD_Autoskillchecks.MWN.View
{
	/// <summary>
	/// Interaktionslogik für DebugView.xaml
	/// </summary>
	public partial class DebugView : UserControl
	{
		private TargetDirectory targetDirectory = new TargetDirectory();

		public DebugView()
		{
			InitializeComponent();
			var w = Path.Combine(targetDirectory.TargetPath, "debug.bmp");
			ScaleImg(ImageSearchUtil.GetBitmapFromFile(w), 280, 280);
			UpdateImage();
		}

		private void ScaleImg(Bitmap image, int width, int height)
		{
			var destRect = new Rectangle(0, 0, width, height);
			var destImage = new Bitmap(width, height);

			destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

			using (var graphics = Graphics.FromImage(destImage))
			{
				graphics.CompositingMode = CompositingMode.SourceCopy;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

				using (var wrapMode = new ImageAttributes())
				{
					wrapMode.SetWrapMode(WrapMode.TileFlipXY);
					graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
				}
			}
			var w = Path.Combine(targetDirectory.TargetPath, "debugscaled.bmp");
			destImage.Save(w,ImageFormat.Bmp);
		}

		private void UpdateImage()
		{
			var path = Path.Combine(targetDirectory.TargetPath, "debugscaled.bmp");
			BitmapImage _image = new BitmapImage();
			_image.BeginInit();
			_image.CacheOption = BitmapCacheOption.None;
			_image.UriCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
			_image.CacheOption = BitmapCacheOption.OnLoad;
			_image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
			_image.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
			_image.EndInit();
			bmpimage.Source = _image;
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			UpdateImage();
		}

		private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
		{
			CoreHandler.DebugRoutine();
		}
	}
}