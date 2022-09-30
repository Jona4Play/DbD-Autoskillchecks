using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbD_Autoskillchecks.Core;
namespace DbD_Autoskillchecks.MWN.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand {get; set; }
        public RelayCommand SkillcheckCommand {get; set; }

        public HomeViewModel HomeVM { get; set; }
        public SkillcheckViewModel SkillcheckVM { get; set; }
        private object _currentView;
         
        
        public object CurrentView
        {
            get { return _currentView; }

            set { 
                _currentView = value; 
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            
            SkillcheckVM = new SkillcheckViewModel();

            HomeViewCommand = new RelayCommand(o => {
                CurrentView = HomeVM;
            });
            SkillcheckCommand = new RelayCommand(o => {
                CurrentView = SkillcheckVM;
            });
        }
        



    }
}
