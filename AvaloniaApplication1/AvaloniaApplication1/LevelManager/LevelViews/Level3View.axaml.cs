using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.LevelManager.LevelModels;

namespace AvaloniaApplication1.Views;

public partial class Level3View : UserControl
{
    public Level3View()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private Level3Model? L3M => DataContext as Level3Model;
}