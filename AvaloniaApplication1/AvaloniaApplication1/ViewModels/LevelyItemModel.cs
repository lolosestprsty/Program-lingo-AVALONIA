using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApplication1.ViewModels
{
    public class LevelyItemModel : ObservableObject
    {
        string _levelName;
        public string LevelName
        {
            get => _levelName;
            set => SetProperty(ref _levelName, value);
        }
        string _levelDescription;
        public string LevelDescription
        {
            get => _levelDescription;
            set => SetProperty(ref _levelDescription, value);
        }
        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }
        
        private int _index;
        public int Index
        {
            get => _index;
            set => SetProperty(ref _index, value);
        }
    }
}