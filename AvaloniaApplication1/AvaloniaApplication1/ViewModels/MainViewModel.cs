using AvaloniaApplication1.LevelManager.LevelModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            currentViewModel = new LevelyOverviewModel(this);
        }

        [ObservableProperty]
        private ObservableObject currentViewModel;

        [RelayCommand]
        private void ShowLevelyOverview()
        {
            CurrentViewModel = new LevelyOverviewModel(this);
        }

        [RelayCommand]
        private void ShowLevel1()
        {
            CurrentViewModel = new Level1Model(this);
        }

        [ObservableProperty]
        private bool level1Completed;

        [RelayCommand]
        private void ShowLevel2()
        {
            CurrentViewModel = new Level2Model(this);
        }

        [ObservableProperty]
        private bool level2Completed;

        [RelayCommand]
        private void ShowLevel3()
        {
            CurrentViewModel = new Level3Model(this);
        }

        [ObservableProperty]
        private bool level3Completed;

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
