using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private MainViewModel? VM => DataContext as MainViewModel;

        private void Levely_Click(object? sender, PointerPressedEventArgs e)
        {
            VM?.ShowLevelyOverviewCommand.Execute(null);
        }

        private void Vysvetlivky_Click(object? sender, PointerPressedEventArgs e)
        {
            VM?.ShowVysvetlivkyOverviewCommand.Execute(null);
        }

        private void Nastavenia_Click(object? sender, PointerPressedEventArgs e)
        {
            VM?.ShowNastaveniaOverviewCommand.Execute(null);
        }
    }
}
