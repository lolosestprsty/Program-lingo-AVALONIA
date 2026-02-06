using AvaloniaApplication1.ViewModels;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.LevelManager.LevelModels
{
    public partial class Level1Model : ViewModelBase
    {
        private readonly MainViewModel _main;

        public Level1Model(MainViewModel main)
        {
            _main = main;
        }

        public IRelayCommand BackCommand =>
            new RelayCommand(() =>
            {
                _main.ShowLevelyOverviewCommand.Execute(null);
            });
    }
}
