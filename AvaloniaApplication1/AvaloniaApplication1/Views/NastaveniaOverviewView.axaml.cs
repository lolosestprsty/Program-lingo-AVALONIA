using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Avalonia.Controls.ApplicationLifetimes;
using System;

namespace AvaloniaApplication1.Views
{
    public partial class NastaveniaOverviewView : UserControl
    {
        public NastaveniaOverviewView()
        {
            InitializeComponent();
            this.AttachedToVisualTree += OnAttached;
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnAttached(object? sender, VisualTreeAttachmentEventArgs e)
        {
            var exitBtn = this.FindControl<Button>("ExitButton");
            if (exitBtn != null)
                exitBtn.Click += ExitBtn_Click;
        }

        private async void ExitBtn_Click(object? sender, RoutedEventArgs e)
        {
            var owner = this.VisualRoot as Window;
            if (owner is null)
                return;

            var tcs = new System.Threading.Tasks.TaskCompletionSource<bool>();

            var buttonTheme = Application.Current?.Resources["ButtonTheme"] as Avalonia.Styling.ControlTheme;

            var yesBtn = new Button 
            { 
                Content = "Áno", 
                Width = 90,
                Theme = buttonTheme,
                HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalContentAlignment = Avalonia.Layout.VerticalAlignment.Center
            };
            var noBtn = new Button 
            { 
                Content = "Nie", 
                Width = 90,
                Theme = buttonTheme,
                HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalContentAlignment = Avalonia.Layout.VerticalAlignment.Center
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
                            TextAlignment = Avalonia.Media.TextAlignment.Center,
                            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
                        },
                        new StackPanel
                        {
                            Orientation = Avalonia.Layout.Orientation.Horizontal,
                            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                            Spacing = 10,
                            Children = { yesBtn, noBtn }
                        }
                    }
                }
            };

            yesBtn.Click += (_, __) => { tcs.TrySetResult(true); dlg.Close(); };
            noBtn.Click += (_, __) => { tcs.TrySetResult(false); dlg.Close(); };

            await dlg.ShowDialog(owner);
            var result = await tcs.Task;
            if (result)
            {
                if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                    desktop.Shutdown();
            }
        }
    }
    
}