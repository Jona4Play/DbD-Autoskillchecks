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
			gracepixels.Value = SaveFile.ReturnPropertyValueByName("GraceZone");
			searchcircle.Value = SaveFile.ReturnPropertyValueByName("SearchCircle");
			delayms.Value = SaveFile.ReturnPropertyValueByName("DelayInMS");
			skillcheckradius.Value = SaveFile.ReturnPropertyValueByName("SkillcheckRadius");
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			SaveFile.ClearList();
			SaveFile.AddProperty("GraceZone", (int)gracepixels.Value);
			SaveFile.AddProperty("SearchCircle", (int)searchcircle.Value);
			SaveFile.AddProperty("DelayInMS", (int)delayms.Value);
			SaveFile.AddProperty("SkillcheckRadius", (int)skillcheckradius.Value);
			SaveFile.SaveToFile();
		}
	}
}