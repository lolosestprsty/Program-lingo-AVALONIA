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
    }
}