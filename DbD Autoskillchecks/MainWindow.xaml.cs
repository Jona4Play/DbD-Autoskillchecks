using System;
using System.Windows;
using System.Runtime.InteropServices;
using System.Windows.Input;
using DbD_Autoskillchecks.Core;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.VisualStyles;
using System.Globalization;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using MessageBox = System.Windows.Forms.MessageBox;
using KeyEventHandler = System.Windows.Input.KeyEventHandler;
using MouseEventHandler = System.Windows.Input.MouseEventHandler;
using System.IO;

namespace DbD_Autoskillchecks
{
     
    public partial class MainWindow : Window
    {
        public bool shouldexecute = false;
        public bool shouldcheck = false;
        [DllImport("user32.dll")]
            public static extern IntPtr FindWindow(
            string ClassName,   
            string WindowName);

        [DllImport("User32.dll")]
        public static extern IntPtr FindWindowEx(
            IntPtr Parent,
            IntPtr Child,
            string lpszClass,
            string lpszWindows);

        [DllImport("User32.dll")]
        public static extern Int32 PostMessage(
            IntPtr hWnd,
            int Msg,
            int wParam,
            int lParam);
        Skillcheckbot sc = new Skillcheckbot();

        public MainWindow()
        {
            InitializeComponent();
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
            sc.SkillcheckExecute(true);
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
        private void runai_Checked(object sender, RoutedEventArgs e)
        {
            shouldexecute = (bool)runai.IsChecked;
            Task.Run(() => ExecuteSkillcheck());
        }
        private void ExecuteSkillcheck()
        {
            while(shouldexecute)
            {
                sc.SkillcheckExecute(false);
            }
        }
        private void CheckKeys()
        {
            while (shouldexecute)
            {
                
            }
        }

        private void runai_Unchecked(object sender, RoutedEventArgs e)
        {
            shouldexecute = false;
        }

        private void checkkeys_Checked(object sender, RoutedEventArgs e)
        {
            shouldcheck = true;
            Task.Run(() => CheckKeys());
        }

        private void checkkeys_Unchecked(object sender, RoutedEventArgs e)
        {
            shouldcheck = false;
        }
        private void SaveOnClick(object sender, RoutedEventArgs e)
        {
            WriteSaveFile writeSaveFile = new WriteSaveFile();
            writeSaveFile.SaveToFile();
        }
    }
}