using DbD_Autoskillchecks.Core;
using DbD_Autoskillchecks.Core.Files;
using DbD_Autoskillchecks.MWN.View;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using CheckBox = System.Windows.Forms.CheckBox;

namespace DbD_Autoskillchecks
{
	public partial class MainWindow : Window
	{
		private bool shouldexecute = false;
		private bool shouldcheck = false;


		private Skillcheckbot sc = new Skillcheckbot();
		private HomeView homeView = new HomeView();

		public MainWindow()
		{
			InitializeComponent();
		}


		public void AddToList()
		{
			Console.WriteLine("Pressed S");
		}

		private void GridMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				DragMove();
			}
		}

		private void OnButtonSearchPixels(object sender, RoutedEventArgs e)
		{
			SaveFile saveFile = new SaveFile();
			saveFile.AddProperty(280, "MinRemainingPixels");
			saveFile.AddProperty(232, "Retry");
			saveFile.AddProperty(280, "DelayFrame");
			saveFile.AddProperty(280, "DE");
			saveFile.SaveToFile();
			//sc.SkillcheckExecute(true);
		}

		private void CheckBox_CheckedChangedAsync(object sender, RoutedEventArgs e)
		{
			bool ischecked = (bool)runai.IsChecked;

			if ((bool)runai.IsChecked)
			{
				Task.Run(() => sc.SkillcheckExecute(false));
			}
		}

		private void QuitButton_Checked(object sender, RoutedEventArgs e)
		{
			System.Windows.Application.Current.Shutdown();
		}

		public bool isChecked(CheckBox checkbox)
		{
			if (checkbox.CheckState == CheckState.Checked) return true;
			else return false;
		}

		//Check if Checkbox is checked
		private void runai_Checked(object sender, RoutedEventArgs e)
		{
			shouldexecute = (bool)runai.IsChecked;
			Task.Run(() => ExecuteSkillcheck());
		}

		private void ExecuteSkillcheck()
		{
			while (shouldexecute)
			{
				sc.SkillcheckExecute(false);
				Console.WriteLine("Executing Skillchecks");
			}
		}

		private void runai_Unchecked(object sender, RoutedEventArgs e)
		{
			shouldexecute = false;
		}

		//CheckKeys for C
		private void checkkeys_Checked(object sender, RoutedEventArgs e)
		{
			shouldcheck = (bool)checkkeys.IsChecked;
			Task.Run(() => Checkkeys());
		}

		private void Checkkeys()
		{
			while (shouldcheck)
			{
				if (IsKeyPushedDown(Keys.C))
				{
					Console.WriteLine("Found C Press");
					sc.SkillcheckExecute(true);
				}
			}
		}

		private void checkkeys_Unchecked(object sender, RoutedEventArgs e)
		{
			shouldcheck = false;
		}

		private void SaveOnClick(object sender, RoutedEventArgs e)
		{
			WriteSaveFile writeSaveFile = new WriteSaveFile();
		}

		[DllImport("user32.dll")]
		private static extern short GetAsyncKeyState(Keys vKey);

		public static bool IsKeyPushedDown(System.Windows.Forms.Keys vKey)
		{
			var pressed = 0 != (GetAsyncKeyState((Keys)(int)vKey) & 0x8000);
			return pressed;
		}
	}
}