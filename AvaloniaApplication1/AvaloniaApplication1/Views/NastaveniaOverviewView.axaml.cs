using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia;

namespace AvaloniaApplication1.Views
{
    public partial class NastaveniaOverviewView : UserControl
    {
        public NastaveniaOverviewView()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
    
}