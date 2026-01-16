using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using AvaloniaApplication1.ViewModels;
using System;

namespace AvaloniaApplication1.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private MainViewModel? VM => DataContext as MainViewModel;
        // Matches PointerPressed="Levely_Click" in XAML
        private void Levely_Click(object? sender, PointerPressedEventArgs e)
        {
            VM?.ShowLevelyOverviewCommand.Execute(null);

        }

        // Matches PointerPressed="Vysvetlivky_Click" in XAML
        private void Vysvetlivky_Click(object? sender, PointerPressedEventArgs e)
        {
            VM?.ShowVysvetlivkyOverviewCommand.Execute(null);
        }

        // Matches PointerPressed="Nastavenia_Click" in XAML
        private void Nastavenia_Click(object? sender, PointerPressedEventArgs e)
        {
            VM?.ShowNastaveniaOverviewCommand.Execute(null);
        }

        private void TextBlock_ActualThemeVariantChanged(object? sender, EventArgs e)
        {
        }
    }
}
