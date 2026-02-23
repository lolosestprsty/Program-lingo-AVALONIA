using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Threading.Tasks;

namespace AvaloniaApplication1.Views
{
    public partial class MessageBoxWindow : Window
    {
        public MessageBoxWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public static async Task<bool> Show(Window owner, string text, string title)
        {
            var dlg = new MessageBoxWindow
            {
                Title = title,
                Width = 400,
                Height = 150,
                Content = new StackPanel
                {
                    Margin = new Thickness(12),
                    Children =
                    {
                        new TextBlock { Text = text },
                        new StackPanel
                        {
                            Orientation = Avalonia.Layout.Orientation.Horizontal,
                            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                            Spacing = 10,
                            Children =
                            {
                                new Button { Name = "YesBtn", Content = "Áno", Width = 80 },
                                new Button { Name = "NoBtn", Content = "Nie", Width = 80 }
                            }
                        }
                    }
                }
            };

            // create buttons and wire result
            var yes = new Button { Content = "Áno", Width = 80 };
            var no = new Button { Content = "Nie", Width = 80 };

            var tcs = new TaskCompletionSource<bool?>();
            yes.Click += (_, __) => tcs.TrySetResult(true);
            no.Click += (_, __) => tcs.TrySetResult(false);

            // place buttons into dialog content (find the inner StackPanel)
            if (dlg.Content is StackPanel root && root.Children.Count >= 2 && root.Children[1] is StackPanel btnPanel)
            {
                btnPanel.Children.Clear();
                btnPanel.Children.Add(yes);
                btnPanel.Children.Add(no);
            }

            await dlg.ShowDialog(owner);
            var result = await tcs.Task;
            return result == true;
        }
    }
}
