using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace AvaloniaApplication1.Views
{
    public partial class LevelyOverviewView : UserControl
    {
        public LevelyOverviewView()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

    }
}

