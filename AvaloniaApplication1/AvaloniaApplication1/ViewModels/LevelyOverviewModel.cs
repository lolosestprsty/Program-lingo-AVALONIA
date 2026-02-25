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
                e.PropertyName == nameof(MainViewModel.Level4Completed) ||
                e.PropertyName == nameof(MainViewModel.Level5Completed) ||
                e.PropertyName == nameof(MainViewModel.Level6Completed) ||
                e.PropertyName == nameof(MainViewModel.Level7Completed) ||
                e.PropertyName == nameof(MainViewModel.Level8Completed) ||
                e.PropertyName == nameof(MainViewModel.Level9Completed) ||
                e.PropertyName == nameof(MainViewModel.Level10Completed) ||
                e.PropertyName == nameof(MainViewModel.Level11Completed) ||
                e.PropertyName == nameof(MainViewModel.Level12Completed) ||
                e.PropertyName == nameof(MainViewModel.Level1Failed) ||
                e.PropertyName == nameof(MainViewModel.Level2Failed) ||
                e.PropertyName == nameof(MainViewModel.Level3Failed) ||
                e.PropertyName == nameof(MainViewModel.Level4Failed) ||
                e.PropertyName == nameof(MainViewModel.Level5Failed) ||
                e.PropertyName == nameof(MainViewModel.Level6Failed) ||
                e.PropertyName == nameof(MainViewModel.Level7Failed) ||
                e.PropertyName == nameof(MainViewModel.Level8Failed) ||
                e.PropertyName == nameof(MainViewModel.Level9Failed) ||
                e.PropertyName == nameof(MainViewModel.Level10Failed) ||
                e.PropertyName == nameof(MainViewModel.Level11Failed) ||
                e.PropertyName == nameof(MainViewModel.Level12Failed))
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
            if (_main.Level1Failed || _main.Level2Failed || _main.Level3Failed || _main.Level4Failed || _main.Level5Failed || _main.Level6Failed || _main.Level7Failed || _main.Level8Failed || _main.Level9Failed || _main.Level10Failed || _main.Level11Failed || _main.Level12Failed)
            {
                imageName = "PaniP-Sad.png";
                message = "Hupsi, škoda. Skúsiš to znovu?";
            }
            // Ak je aspoň jeden level úspešný (completed), zobraziť PaniP-Happy
            else if (_main.Level1Completed || _main.Level2Completed || _main.Level3Completed || _main.Level4Completed || _main.Level5Completed || _main.Level6Completed || _main.Level7Completed || _main.Level8Completed || _main.Level9Completed || _main.Level10Completed || _main.Level11Completed || _main.Level12Completed)
            {
                imageName = "PaniP-Happy.png";
                // Zistíme číslo najvyššieho dokončeného levelu
                int completedLevel = 0;
                if (_main.Level12Completed) completedLevel = 12;
                else if (_main.Level11Completed) completedLevel = 11;
                else if (_main.Level10Completed) completedLevel = 10;
                else if (_main.Level9Completed) completedLevel = 9;
                else if (_main.Level8Completed) completedLevel = 8;
                else if (_main.Level7Completed) completedLevel = 7;
                else if (_main.Level6Completed) completedLevel = 6;
                else if (_main.Level5Completed) completedLevel = 5;
                else if (_main.Level4Completed) completedLevel = 4;
                else if (_main.Level3Completed) completedLevel = 3;
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
                    else if (n == 4)
                        item.IsEnabled = _main.Level3Completed;
                    else if (n == 5)
                        item.IsEnabled = _main.Level4Completed;
                    else if (n == 6)
                        item.IsEnabled = _main.Level5Completed;
                    else if (n == 7)
                        item.IsEnabled = _main.Level6Completed;
                    else if (n == 8)
                        item.IsEnabled = _main.Level7Completed;
                    else if (n == 9)
                        item.IsEnabled = _main.Level8Completed;
                    else if (n == 10)
                        item.IsEnabled = _main.Level9Completed;
                    else if (n == 11)
                        item.IsEnabled = _main.Level10Completed;
                    else if (n == 12)
                        item.IsEnabled = true; // Temporarily enabled for testing - change back to: _main.Level11Completed
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
                else if (level?.LevelName == "4")
                {
                    _main.ShowLevel4Command.Execute(null);
                }
                else if (level?.LevelName == "5")
                {
                    _main.ShowLevel5Command.Execute(null);
                }
                else if (level?.LevelName == "6")
                {
                    _main.ShowLevel6Command.Execute(null);
                }
                else if (level?.LevelName == "7")
                {
                    _main.ShowLevel7Command.Execute(null);
                }
                else if (level?.LevelName == "8")
                {
                    _main.ShowLevel8Command.Execute(null);
                }
                else if (level?.LevelName == "9")
                {
                    _main.ShowLevel9Command.Execute(null);
                }
                else if (level?.LevelName == "10")
                {
                    _main.ShowLevel10Command.Execute(null);
                }
                else if (level?.LevelName == "11")
                {
                    _main.ShowLevel11Command.Execute(null);
                }
                else if (level?.LevelName == "12")
                {
                    _main.ShowLevel12Command.Execute(null);
                }
            });

        #endregion
        #region Methods
        private void LoadLevels()
        {
            LevelCollection = new ObservableCollection<LevelyItemModel>(); // Initialize LevelCollection

            for (int i = 1; i <= 12; i++)
            {
                LevelCollection.Add(new LevelyItemModel
                {
                    LevelName = i.ToString(),
                    Index = i - 1, // 0-based index pre converter
                    IsEnabled = i == 1 // only level 1 enabled initially
                });
            }
        }
        #endregion

    }
}
