using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1.Views
{
    public partial class VysvetlivkyOverviewView : UserControl
    {
        public VysvetlivkyOverviewView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private VysvetlivkyOverviewModel? VOM=>DataContext as VysvetlivkyOverviewModel;
        private void Vysvetlivka1_Click(object? sender, PointerPressedEventArgs e)
        {
            VOM?.ShowVysvetlivka1Command.Execute(null);
        }
        private void Vysvetlivka2_Click(object? sender, PointerPressedEventArgs e)
        {
            VOM?.ShowVysvetlivka2Command.Execute(null);
        }
    private void Vysvetlivka3_Click(object? sender, PointerPressedEventArgs e)
    {
        VOM?.ShowVysvetlivka3Command.Execute(null);
    }
    private void Vysvetlivka4_Click(object? sender, PointerPressedEventArgs e)
    {
        VOM?.ShowVysvetlivka4Command.Execute(null);
    }
    private void Vysvetlivka5_Click(object? sender, PointerPressedEventArgs e)
    {
        VOM?.ShowVysvetlivka5Command.Execute(null);
    }
    private void Vysvetlivka6_Click(object? sender, PointerPressedEventArgs e)
    {
        VOM?.ShowVysvetlivka6Command.Execute(null);
    }
    private void Vysvetlivka7a_Click(object? sender, PointerPressedEventArgs e)
    {
        VOM?.ShowVysvetlivka7aCommand.Execute(null);
    }
    private void Vysvetlivka7b_Click(object? sender, PointerPressedEventArgs e)
    {
        VOM?.ShowVysvetlivka7bCommand.Execute(null);
    }
    private void Vysvetlivka7_Click(object? sender, PointerPressedEventArgs e)
    {
        VOM?.ShowVysvetlivka7Command.Execute(null);
    }
    private void Vysvetlivka8_Click(object? sender, PointerPressedEventArgs e)
    {
        VOM?.ShowVysvetlivka8Command.Execute(null);
    }
    private void Vysvetlivka9_Click(object? sender, PointerPressedEventArgs e)
    {
        VOM?.ShowVysvetlivka9Command.Execute(null);
    }
    private void Vysvetlivka10_Click(object? sender, PointerPressedEventArgs e)
    {
        VOM?.ShowVysvetlivka10Command.Execute(null);
    }
    private void Vysvetlivka11_Click(object? sender, PointerPressedEventArgs e)
    {
        VOM?.ShowVysvetlivka11Command.Execute(null);
    }
    private void Vysvetlivka12_Click(object? sender, PointerPressedEventArgs e)
    {
        VOM?.ShowVysvetlivka12Command.Execute(null);
    }
    private void Vysvetlivka13_Click(object? sender, PointerPressedEventArgs e)
    {
        VOM?.ShowVysvetlivka13Command.Execute(null);
    }
    }
}