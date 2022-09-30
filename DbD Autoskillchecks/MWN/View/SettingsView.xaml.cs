using DbD_Autoskillchecks.Core;
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
            ReadSaveFile rsv = new ReadSaveFile();
            remainingpixels.Value = rsv.MinRemainingPixels;
            delayframes.Value = rsv.DelayFrame;
            tolerance.Value = rsv.Tolerance;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WriteSaveFile wsf = new WriteSaveFile();
            wsf.SaveToFile((int)remainingpixels.Value, (int)tolerance.Value, (int)delayframes.Value);
        }
    }
}