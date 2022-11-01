using DbD_Autoskillchecks.Core;
using DbD_Autoskillchecks.Core.Files;
using System.Windows;
using System.Windows.Controls;

namespace DbD_Autoskillchecks.MWN.View
{
	/// <summary>
	/// Interaktionslogik für SettingsView.xaml
	/// </summary>
	public partial class SettingsView : UserControl
	{
		public SettingsView()
		{
			InitializeComponent();
			OnInitializeChangeValues();
		}

		private void OnInitializeChangeValues()
		{
			SaveFile saveFile = new SaveFile();
			ReadSaveFile rsv = new ReadSaveFile();
			overlappixels.Value = rsv.OverlapPixels;
			delayframes.Value = rsv.DelayFrame;
			tolerance.Value = rsv.Tolerance;
			minremainingpixels.Value = rsv.MinRemainingPixels;
			MoonwalkDelay.Value = rsv.MoonwalkDelay;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			WriteSaveFile wsf = new WriteSaveFile();
			wsf.SaveToFile((int)overlappixels.Value, (int)tolerance.Value, (int)delayframes.Value, (int)minremainingpixels.Value, (int)MoonwalkDelay.Value);
		}
	}
}