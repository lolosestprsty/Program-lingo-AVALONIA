using System;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.ViewModels
{
    public partial class EnterModel : ViewModelBase
    {
        private readonly MainViewModel _main;

        public EnterModel(MainViewModel main)
        {
            _main = main;
            LoadEnterImage();
        }

        [ObservableProperty]
        private Bitmap? paniPImage;

        private void LoadEnterImage()
        {
            try
            {
                var uri = new Uri("avares://AvaloniaApplication1/Assets/PaniP-ENTER.png");
                paniPImage = new Bitmap(AssetLoader.Open(uri));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load PaniP-ENTER.png: {ex.Message}");
            }
        }

        [RelayCommand]
        private void EnterApp()
        {
            _main.ShowLevelyOverviewCommand.Execute(null);
        }
    }
}
