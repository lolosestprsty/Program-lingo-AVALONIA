using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace AvaloniaApplication1.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            currentViewModel = new LevelyOverviewModel();
        }

        [ObservableProperty]
        private ObservableObject currentViewModel;

        [RelayCommand]
        private void ShowLevelyOverview()
        {
            CurrentViewModel = new LevelyOverviewModel();
        }

        [RelayCommand]
        private void ShowVysvetlivkyOverview()
        {
            CurrentViewModel = new VysvetlivkyOverviewModel();
        }
        [RelayCommand]
        private void ShowNastaveniaOverview()
        {
            CurrentViewModel = new NastaveniaOverviewModel();
        }

        //[RelayCommand(CanExecute =nameof (CanActivatePage))]


    }
}
