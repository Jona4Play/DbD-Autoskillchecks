using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DbD_Autoskillchecks.Core.Keys
{
	internal class SendKeys
	{
		[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
		public static extern IntPtr FindWindow(IntPtr ZeroOnly, string lpWindowName);

		[DllImport("user32.dll")]
		public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, IntPtr dwExtraInfo);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetForegroundWindow(IntPtr hWnd);
		public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
		public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
		public const int VK_SPACE = 0x20; //Space Key Code

		public void SendKeysToDbD(byte VK_KEYCODE)
		{
			int processID = 0;
			Process[] process = Process.GetProcessesByName("DeadByDaylight-Win64-Shipping");
			if (process.Length == 1)
			{
				foreach (var proc in process)
				{
					processID = proc.Id;
				}
			}
			System.IntPtr MainHandle = Process.GetProcessById(processID).MainWindowHandle;
			keybd_event(VK_SPACE, 0, KEYEVENTF_EXTENDEDKEY, MainHandle);
			Task.Delay(2);
			keybd_event(VK_SPACE, 0, KEYEVENTF_KEYUP, MainHandle);
		}
	}
}