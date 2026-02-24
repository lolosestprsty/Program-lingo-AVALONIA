using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.LevelManager.LevelModels;

namespace AvaloniaApplication1.Views;

public partial class Level2View : UserControl
{
    public Level2View()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private Level2Model? L2M => DataContext as Level2Model;
}