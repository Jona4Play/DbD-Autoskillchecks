using DbD_Autoskillchecks.Core;
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace DbD_Autoskillchecks.MWN.View
{
	/// <summary>
	/// Interaktionslogik für MoonwalkView.xaml
	/// </summary>
	public partial class MoonwalkView : System.Windows.Controls.UserControl
	{
		[DllImport("user32.dll", SetLastError = true)]
		private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
		public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
		public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
		public const int VK_D = 0x44; //Space Key Code
		public const int VK_A = 0x41; //Space Key Code
		private static bool shouldexecute = false;
		ReadSaveFile rsv = new ReadSaveFile();
		public MoonwalkView()
		{
			InitializeComponent();
		}

		private void CheckKeys_Checked(object sender, System.Windows.RoutedEventArgs e)
		{
			shouldexecute = true;
			Task.Run(() => ExecuteMoonWalk());
		}

		private void CheckKeys_Unchecked(object sender, System.Windows.RoutedEventArgs e)
		{
			shouldexecute = false;
			Task.Run(() => ExecuteMoonWalk());
		}
		private void ExecuteMoonWalk()
		{
			while (IsKeyPushedDown(Keys.F))
			{
				pressA();
				Task.Delay(rsv.MoonwalkDelay).Wait();
				pressD();
				Task.Delay(rsv.MoonwalkDelay).Wait();
			}
		}
		[DllImport("user32.dll")]
		private static extern short GetAsyncKeyState(Keys vKey);

		public static bool IsKeyPushedDown(System.Windows.Forms.Keys vKey)
		{
			var pressed = 0 != (GetAsyncKeyState((Keys)(int)vKey) & 0x8000);
			return pressed;
		}
		private void pressA()
		{
			keybd_event(VK_A, 0, KEYEVENTF_EXTENDEDKEY, 0);
			Task.Delay(2);
			keybd_event(VK_A, 0, KEYEVENTF_KEYUP, 0);
			Console.WriteLine("Pressed A");
		}
		private void pressD()
		{
			keybd_event(VK_D, 0, KEYEVENTF_EXTENDEDKEY, 0);
			Task.Delay(2);
			keybd_event(VK_D, 0, KEYEVENTF_KEYUP, 0);
			Console.WriteLine("Pressed D");
		}
	}
}