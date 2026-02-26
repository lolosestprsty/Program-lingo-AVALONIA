using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace AvaloniaApplication1.Views
{
    public partial class LevelyOverviewView : UserControl
    {
        private ScrollViewer? _scrollViewer;
        private ScrollBar? _scrollBar;

        public LevelyOverviewView()
        {
            InitializeComponent();
            this.AttachedToVisualTree += OnAttached;
        }

        private void OnAttached(object? sender, VisualTreeAttachmentEventArgs e)
        {
            _scrollViewer = this.FindControl<ScrollViewer>("levelsScrollViewer");
            _scrollBar = this.FindControl<ScrollBar>("levelsScrollBar");
            
            if (_scrollViewer is { } sv && _scrollBar is { } sb)
            {
                sv.LayoutUpdated += (s, ev) =>
                {
                    var extent = sv.Extent.Width;
                    var viewport = sv.Viewport.Width;
                    sb.Maximum = Math.Max(0, extent - viewport);
                    sb.ViewportSize = viewport;
                };

                sb.PropertyChanged += (s, ev) =>
                {
                    if (ev.Property == RangeBase.ValueProperty)
                    {
                        sv.Offset = new Vector(sb.Value, sv.Offset.Y);
                    }
                };
            }
        }
    }
}

