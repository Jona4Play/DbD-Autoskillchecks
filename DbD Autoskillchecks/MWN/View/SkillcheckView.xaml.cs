using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DbD_Autoskillchecks.Core;

namespace DbD_Autoskillchecks.MWN.View
{
    
    /// <summary>
    /// Interaktionslogik für Skillcheck.xaml
    /// </summary>
    public partial class Skillcheck : UserControl
    {
        Skillcheckbot skillcheckbot = new Skillcheckbot();
        bool isRunning = false;
        public Skillcheck()
        {
            InitializeComponent();
            
        }

        

        

        private void Debug_Click(object sender, RoutedEventArgs e)
        {
            skillcheckbot.SkillcheckExecute(true);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            while (runia.IsChecked == true)
            {
                skillcheckbot.SkillcheckExecute(false);
            }
        }
    }
}
