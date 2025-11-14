using CommunityToolkit.Mvvm.ComponentModel;

namespace Program_lingo_AVALONIA.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia!";
}
