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
