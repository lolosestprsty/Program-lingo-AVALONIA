using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.LevelManager.LevelModels;

namespace AvaloniaApplication1.Views;

public partial class Level1View : UserControl
{
    public Level1View()
    {
        InitializeComponent();
    }   

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private Level1Model? L1M => DataContext as Level1Model;
}