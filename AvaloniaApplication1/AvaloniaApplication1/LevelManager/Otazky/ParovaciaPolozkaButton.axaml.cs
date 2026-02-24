using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using System;

namespace AvaloniaApplication1.LevelManager.Otazky
{
    public partial class ParovaciaPolozkaButton : UserControl
    {
        public static readonly StyledProperty<bool> IsLeftProperty =
            AvaloniaProperty.Register<ParovaciaPolozkaButton, bool>(nameof(IsLeft));

        public bool IsLeft
        {
            get => GetValue(IsLeftProperty);
            set => SetValue(IsLeftProperty, value);
        }

        private Button? _mainButton;

        public ParovaciaPolozkaButton()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _mainButton = this.FindControl<Button>("MainButton");
        }

        private void OnDataContextChanged(object? sender, EventArgs e)
        {
            if (DataContext is ParovaciaPolozka polozka)
            {
                polozka.PropertyChanged += Polozka_PropertyChanged;
                UpdateButtonStyle();
            }
        }

        private void Polozka_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ParovaciaPolozka.IsSelected) ||
                e.PropertyName == nameof(ParovaciaPolozka.IsMatched) ||
                e.PropertyName == nameof(ParovaciaPolozka.IsError))
            {
                UpdateButtonStyle();
            }
        }

        private void UpdateButtonStyle()
        {
            if (_mainButton == null || DataContext is not ParovaciaPolozka polozka)
                return;

            if (polozka.IsMatched)
            {
                _mainButton.BorderBrush = new SolidColorBrush(Color.Parse("#5CAD61")); // Green
                _mainButton.BorderThickness = new Thickness(3);
                _mainButton.Background = new SolidColorBrush(Color.Parse("#E8F5E9")); // Light green
            }
            else if (polozka.IsError)
            {
                _mainButton.BorderBrush = new SolidColorBrush(Color.Parse("#E53935")); // Red
                _mainButton.BorderThickness = new Thickness(3);
                _mainButton.Background = new SolidColorBrush(Color.Parse("#FFEBEE")); // Light red
            }
            else if (polozka.IsSelected)
            {
                _mainButton.BorderBrush = new SolidColorBrush(Color.Parse("#5CAD61")); // Green instead of blue
                _mainButton.BorderThickness = new Thickness(3);
                _mainButton.Background = new SolidColorBrush(Color.Parse("#E8F5E9")); // Light green
            }
            else
            {
                _mainButton.BorderBrush = new SolidColorBrush(Colors.Gray);
                _mainButton.BorderThickness = new Thickness(2);
                _mainButton.Background = new SolidColorBrush(Color.Parse("#D9D9D9"));
            }
        }

        private void Button_Click(object? sender, RoutedEventArgs e)
        {
            if (DataContext is not ParovaciaPolozka polozka)
                return;

            // Find the parent ParovaciaOtazka
            var parent = this.FindAncestorOfType<ParovaciaOtazkaView>();
            if (parent?.DataContext is ParovaciaOtazka otazka)
            {
                otazka.SelectItem(polozka);
            }
        }
    }
}
