using AvaloniaApplication1.LevelManager.LevelModels;
using AvaloniaApplication1.LevelManager.LevelModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly LevelyOverviewModel _levelyOverviewModel;
        private readonly EnterModel _enterModel;

        public MainViewModel()
        {
            _levelyOverviewModel = new LevelyOverviewModel(this);
            _enterModel = new EnterModel(this);
            currentViewModel = _enterModel;
        }

        [ObservableProperty]
        private ObservableObject currentViewModel;

        [RelayCommand]
        private void ShowLevelyOverview()
        {
            CurrentViewModel = _levelyOverviewModel;
        }

        [RelayCommand]
        private void ShowLevel1()
        {
            CurrentViewModel = new Level1Model(this);
        }

        [ObservableProperty]
        private bool level1Completed;

        [ObservableProperty]
        private bool level1Failed;

        [RelayCommand]
        private void ShowLevel2()
        {
            CurrentViewModel = new Level2Model(this);
        }

        [ObservableProperty]
        private bool level2Completed;

        [ObservableProperty]
        private bool level2Failed;

        [RelayCommand]
        private void ShowLevel3()
        {
            CurrentViewModel = new Level3Model(this);
        }

        [ObservableProperty]
        private bool level3Completed;

        [ObservableProperty]
        private bool level3Failed;

        [RelayCommand]
        private void ShowLevel4()
        {
            CurrentViewModel = new Level4Model(this);
        }

        [ObservableProperty]
        private bool level4Completed;

        [ObservableProperty]
        private bool level4Failed;

        [RelayCommand]
        private void ShowLevel5()
        {
            CurrentViewModel = new Level5Model(this);
        }

        [ObservableProperty]
        private bool level5Completed;

        [ObservableProperty]
        private bool level5Failed;

        [RelayCommand]
        private void ShowLevel6()
        {
            CurrentViewModel = new Level6Model(this);
        }

        [ObservableProperty]
        private bool level6Completed;

        [ObservableProperty]
        private bool level6Failed;

        [RelayCommand]
        private void ShowLevel7()
        {
            CurrentViewModel = new Level7Model(this);
        }

        [ObservableProperty]
        private bool level7Completed;

        [ObservableProperty]
        private bool level7Failed;

        [RelayCommand]
        private void ShowLevel8()
        {
            CurrentViewModel = new Level8Model(this);
        }

        [ObservableProperty]
        private bool level8Completed;

        [ObservableProperty]
        private bool level8Failed;

        [RelayCommand]
        private void ShowLevel9()
        {
            CurrentViewModel = new Level9Model(this);
        }

        [ObservableProperty]
        private bool level9Completed;

        [ObservableProperty]
        private bool level9Failed;

        [RelayCommand]
        private void ShowLevel10()
        {
            CurrentViewModel = new Level10Model(this);
        }

        [ObservableProperty]
        private bool level10Completed;

        [ObservableProperty]
        private bool level10Failed;

        [RelayCommand]
        private void ShowLevel11()
        {
            CurrentViewModel = new Level11Model(this);
        }

        [ObservableProperty]
        private bool level11Completed;

        [ObservableProperty]
        private bool level11Failed;

        [RelayCommand]
        private void ShowLevel12()
        {
            CurrentViewModel = new Level12Model(this);
        }

        [ObservableProperty]
        private bool level12Completed;

        [ObservableProperty]
        private bool level12Failed;


        [RelayCommand]
        private void ShowVysvetlivkyOverview()
        {
            CurrentViewModel = new VysvetlivkyOverviewModel();
        }

        [RelayCommand]
        private void ShowNastaveniaOverview()
        {
            CurrentViewModel = new NastaveniaOverviewModel(this);
        }
    }
}
