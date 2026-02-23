using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.ViewModels
{
    public partial class LevelyOverviewModel : ViewModelBase
    {
        private readonly MainViewModel _main;
        public LevelyOverviewModel(MainViewModel main)
        {
            _main = main;
            // listen for changes on main (to enable levels when prerequisites completed)
            _main.PropertyChanged += Main_PropertyChanged;
            LoadLevels();
            // set initial enabled state based on main
            UpdateLevelAvailability();
        }

        private void Main_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainViewModel.Level1Completed))
            {
                UpdateLevelAvailability();
            }
        }

        private void UpdateLevelAvailability()
        {
            // level 1 always enabled
            for (int i = 0; i < LevelCollection.Count; i++)
            {
                var item = LevelCollection[i];
                if (int.TryParse(item.LevelName, out var n))
                {
                    if (n == 1)
                        item.IsEnabled = true;
                    else if (n == 2)
                        item.IsEnabled = _main.Level1Completed;
                    else
                        item.IsEnabled = false; // other levels locked for now
                }
            }
        }

        #region Properties
        public ObservableCollection<LevelyItemModel> LevelCollection { get; set; }
        #endregion
        #region commands
        public IRelayCommand<LevelyItemModel> LevelSelectedCommand =>
            new RelayCommand<LevelyItemModel>(level =>
            {
                Debug.WriteLine($"Level selected: {level?.LevelName}");
                if (level?.LevelName == "1")
                {
                    _main.ShowLevel1Command.Execute(null);
                }
            });

        #endregion
        #region Methods
        private void LoadLevels()
        {
            LevelCollection = new ObservableCollection<LevelyItemModel>(); // Initialize LevelCollection

            for (int i = 1; i <= 10; i++)
            {
                LevelCollection.Add(new LevelyItemModel
                {
                    LevelName = i.ToString(),
                    IsEnabled = i == 1 // only level 1 enabled initially
                });
            }
        }
        #endregion

    }
}
