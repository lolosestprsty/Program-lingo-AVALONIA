using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaApplication1.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            ShowLevely(); // zobrazí “Levely” po štarte
        }

        private void Levely_Click(object? sender, PointerPressedEventArgs e)
        {
            ShowLevely();
        }

        private void Vysvetlivky_Click(object? sender, PointerPressedEventArgs e)
        {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(new TextBlock
            {
                Text = "Vysvetlivky",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 32,
                Foreground=Brushes.Black
            });

            LevelyText.Opacity = 0.7;
            VysvetlivkyText.Opacity = 1;
            NastaveniaText.Opacity = 0.7;
        }

        private void Nastavenia_Click(object? sender, PointerPressedEventArgs e)
        {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(new TextBlock
            {
                Text = "Nastavenia",
                //Foreground=new SolidColorBrush(Colors.Black),
                Foreground=Brushes.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 32
            });

            LevelyText.Opacity = 0.7;
            VysvetlivkyText.Opacity = 0.7;
            NastaveniaText.Opacity = 1;
        }

        private void ShowLevely()
        {
            ContentArea.Children.Clear();

            var stack = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            stack.Children.Add(new TextBlock
            {
                Text = "Levely",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 24,
                Foreground = Brushes.Black,
                Margin = new Thickness(0, 0, 0, 20)
            });

            var openLevel1Button = new Button
            {
                Content = "Otvoriť Level 1",
                Width = 200,
                Height = 50,
                FontSize = 16,
                Background = new SolidColorBrush(Color.FromRgb(98, 189, 103)),
                Cursor = new Cursor(StandardCursorType.Hand),
                
                HorizontalAlignment = HorizontalAlignment.Center, 
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Foreground= Brushes.Black
            };

            //openLevel1Button.Click += OpenLevel1_Click;
            stack.Children.Add(openLevel1Button);
            Foreground=Brushes.Black;
            ContentArea.Children.Add(stack);

            LevelyText.Opacity = 1;
            VysvetlivkyText.Opacity = 0.7;
            NastaveniaText.Opacity = 0.7;
        }

        /*private void OpenLevel1_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var level1 = new Level1();
            level1.Show();

            // Zatvorí MainWindow
            foreach (var window in Avalonia.Application.Current?.Windows ?? [])
            {
                if (window is MainWindow mainWindow)
                    mainWindow.Close();
            }
        }*/
    }
}
