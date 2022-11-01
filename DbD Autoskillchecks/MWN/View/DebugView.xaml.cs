using DbD_Autoskillchecks.Core;
using System;
using System.ComponentModel;
using System.IO;
using System.Net.Cache;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
			UpdateImage();
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
	}
}