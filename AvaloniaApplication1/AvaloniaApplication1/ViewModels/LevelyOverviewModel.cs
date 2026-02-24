using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
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
            UpdatePaniPImage();
        }

        private void Main_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainViewModel.Level1Completed) ||
                e.PropertyName == nameof(MainViewModel.Level2Completed) ||
                e.PropertyName == nameof(MainViewModel.Level3Completed) ||
                e.PropertyName == nameof(MainViewModel.Level1Failed) ||
                e.PropertyName == nameof(MainViewModel.Level2Failed) ||
                e.PropertyName == nameof(MainViewModel.Level3Failed))
            {
                UpdateLevelAvailability();
                UpdatePaniPImage();
            }
        }

        private void UpdatePaniPImage()
        {
            string imageName;
            string message;
            
            // Ak je aspoň jeden level neúspešný (failed), zobraziť PaniP-Sad
            if (_main.Level1Failed || _main.Level2Failed || _main.Level3Failed)
            {
                imageName = "PaniP-Sad.png";
                message = "Hupsi, škoda. Skúsiš to znovu?";
            }
            // Ak je aspoň jeden level úspešný (completed), zobraziť PaniP-Happy
            else if (_main.Level1Completed || _main.Level2Completed || _main.Level3Completed)
            {
                imageName = "PaniP-Happy.png";
                // Zistíme číslo najvyššieho dokončeného levelu
                int completedLevel = 0;
                if (_main.Level3Completed) completedLevel = 3;
                else if (_main.Level2Completed) completedLevel = 2;
                else if (_main.Level1Completed) completedLevel = 1;
                message = $"Woop woop, úspešne si prešiel {completedLevel}. level!";
            }
            // Inak zobraziť PaniP-ENTER (žiadny level nebol dokončený)
            else
            {
                imageName = "PaniP-ENTER.png";
                message = "Začni hrať!";
            }
            
            PaniPMessage = message;
            
            try
            {
                var uri = new Uri($"avares://AvaloniaApplication1/Assets/{imageName}");
                PaniPImage = new Bitmap(AssetLoader.Open(uri));
                Debug.WriteLine($"PaniP image updated to: {imageName}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load image {imageName}: {ex.Message}");
            }
        }

        private void UpdateLevelAvailability()
        {
            for (int i = 0; i < LevelCollection.Count; i++)
            {
                var item = LevelCollection[i];
                if (int.TryParse(item.LevelName, out var n))
                {
                    if (n == 1)
                        item.IsEnabled = true;
                    else if (n == 2)
                        item.IsEnabled = _main.Level1Completed;
                    else if (n == 3)
                        item.IsEnabled = _main.Level2Completed;
                    else
                        item.IsEnabled = false;
                }
            }
        }

        #region Properties
        public ObservableCollection<LevelyItemModel> LevelCollection { get; set; }

        [ObservableProperty]
        private Bitmap? paniPImage;

        [ObservableProperty]
        private string paniPMessage = string.Empty;
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
                else if (level?.LevelName == "2")
                {
                    _main.ShowLevel2Command.Execute(null);
                }
                else if (level?.LevelName == "3")
                {
                    _main.ShowLevel3Command.Execute(null);
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
