using DbD_Autoskillchecks.Core;

namespace DbD_Autoskillchecks.MWN.ViewModel
{
	internal class MainViewModel : ObservableObject
	{
		public RelayCommand HomeViewCommand { get; set; }
		public RelayCommand SkillcheckCommand { get; set; }
		public RelayCommand MoonwalkCommand { get; set; }
		public RelayCommand SettingsCommand { get; set; }
		public RelayCommand DebugCommand { get; set; }

		public HomeViewModel HomeVM { get; set; }
		public SkillcheckViewModel SkillcheckVM { get; set; }
		public SettingsViewModel SettingsVM { get; set; }
		public DebugViewModel DebugVM { get; set; }
		public MoonWalkViewModel MoonwalkVM { get; set; }
		private object _currentView;

		public object CurrentView
		{
			get { return _currentView; }

			set
			{
				_currentView = value;
				OnPropertyChanged();
			}
		}

		public MainViewModel()
		{
			HomeVM = new HomeViewModel();
			SkillcheckVM = new SkillcheckViewModel();
			MoonwalkVM = new MoonWalkViewModel();
			DebugVM = new DebugViewModel();
			SettingsVM = new SettingsViewModel();

			HomeViewCommand = new RelayCommand(o =>
			{
				CurrentView = HomeVM;
			});
			SkillcheckCommand = new RelayCommand(o =>
			{
				CurrentView = SkillcheckVM;
			});
			DebugCommand = new RelayCommand(o =>
			{
				CurrentView = DebugVM;
			});
			MoonwalkCommand = new RelayCommand(o =>
			{
				CurrentView = MoonwalkVM;
			});
			SettingsCommand = new RelayCommand(o =>
			{
				CurrentView = SettingsVM;
			});
		}
	}
}