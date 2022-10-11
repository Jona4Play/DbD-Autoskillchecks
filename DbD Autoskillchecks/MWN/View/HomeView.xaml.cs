using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace DbD_Autoskillchecks.MWN.View
{
    /// <summary>
    /// Interaktionslogik für HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private bool running;

        public HomeView()
        {
            InitializeComponent();
            Process[] process = Process.GetProcessesByName("DeadByDaylight-Win64-Shipping");
            if (process.Length == 0)
            {
                //Console.WriteLine("Game Not Found");
                WhenWindowNotFound();
            }
            else
            {
                //Console.WriteLine("Game was Found");
                WhenWindowFound();
            }
        }

        public void WhenWindowFound()
        {
            this.Dispatcher.Invoke(() =>
            {
                runningbox.Visibility = Visibility.Visible;
                notrunningbox.Visibility = Visibility.Hidden;
                runningtext.Visibility = Visibility.Visible;
                notrunningtext.Visibility = Visibility.Hidden;
            });
        }

        public void WhenWindowNotFound()
        {
            this.Dispatcher.Invoke(() =>
            {
                runningbox.Visibility = Visibility.Hidden;
                notrunningbox.Visibility = Visibility.Visible;
                runningtext.Visibility = Visibility.Hidden;
                notrunningtext.Visibility = Visibility.Visible;
            });
        }

        public void CheckGameState()
        {
            Process[] process = Process.GetProcessesByName("DeadByDaylight-Win64-Shipping");
            if (process.Length == 0)
            {
                //Console.WriteLine("Game Not Found");
                WhenWindowNotFound();
                GameRunning = false;
            }
            else
            {
                //Console.WriteLine("Game was Found");
                WhenWindowFound();
                GameRunning = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CheckGameState();
        }

        public bool GameRunning
        {
            get { return running; }
            set { running = value; }
        }
    }
}