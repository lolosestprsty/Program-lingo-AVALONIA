using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication1.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // Nastavi? okno na fullscreen (maximalizované)
            WindowState = WindowState.Maximized;
            
            // Volite?ne: Pre skuto?ný fullscreen bez lišty úloh
            // WindowState = WindowState.FullScreen;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}