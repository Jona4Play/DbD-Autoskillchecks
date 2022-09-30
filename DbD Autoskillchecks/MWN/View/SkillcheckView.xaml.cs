using DbD_Autoskillchecks.Core;
using System.Windows;
using System.Windows.Controls;

namespace DbD_Autoskillchecks.MWN.View
{
    /// <summary>
    /// Interaktionslogik für Skillcheck.xaml
    /// </summary>
    public partial class Skillcheck : UserControl
    {
        private Skillcheckbot skillcheckbot = new Skillcheckbot();

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