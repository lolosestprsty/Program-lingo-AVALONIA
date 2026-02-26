using Avalonia.Controls;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.VisualTree;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1.Views
{
    public partial class NastaveniaOverviewView : UserControl
    {
        public NastaveniaOverviewView()
        {
            InitializeComponent();
            this.AttachedToVisualTree += OnAttached;
        }

        private void OnAttached(object? sender, VisualTreeAttachmentEventArgs e)
        {
            var exitBtn = this.FindControl<Button>("ExitButton");
            if (exitBtn != null)
                exitBtn.Click += ExitBtn_Click;
        }

        private void AdminPasswordBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (DataContext is NastaveniaOverviewModel viewModel)
                {
                    if (viewModel.AdminLoginCommand.CanExecute(null))
                    {
                        viewModel.AdminLoginCommand.Execute(null);
                    }
                }
            }
        }

        private async void ExitBtn_Click(object? sender, RoutedEventArgs e)
        {
            var owner = this.VisualRoot as Window;
            if (owner is null)
                return;

            var buttonTheme = Application.Current?.Resources["ButtonTheme"] as Avalonia.Styling.ControlTheme;

            var yesBtn = new Button 
            { 
                Content = "Áno", 
                Width = 90,
                Theme = buttonTheme,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            var noBtn = new Button 
            { 
                Content = "Nie", 
                Width = 90,
                Theme = buttonTheme,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            };

            var dlg = new Window
            {
                Width = 420,
                Height = 150,
                Title = "Potvrdenie",
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                CanResize = false,
                Content = new StackPanel
                {
                    Margin = new Thickness(12),
                    Children =
                    {
                        new TextBlock 
                        { 
                            Text = "Naozaj chcete opusti? aplikáciu?", 
                            Margin = new Thickness(0,0,0,12),
                            TextAlignment = TextAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center
                        },
                        new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Spacing = 10,
                            Children = { yesBtn, noBtn }
                        }
                    }
                }
            };

            var tcs = new System.Threading.Tasks.TaskCompletionSource<bool>();
            yesBtn.Click += (_, __) => { tcs.TrySetResult(true); dlg.Close(); };
            noBtn.Click += (_, __) => { tcs.TrySetResult(false); dlg.Close(); };

            await dlg.ShowDialog(owner);
            var result = await tcs.Task;
            
            if (result && Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown();
            }
        }
    }
}