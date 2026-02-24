using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;

namespace AvaloniaApplication1.LevelManager.Otazky
{
    public partial class ParovaciaPolicka : ObservableObject
    {
        public string Lava { get; set; } = string.Empty;
        public string Prava { get; set; } = string.Empty;

        [ObservableProperty]
        private bool isMatched;
    }

    public partial class ParovaciaPolozka : ObservableObject
    {
        public string Text { get; set; } = string.Empty;
        public int Index { get; set; }
        public bool IsLeft { get; set; }

        [ObservableProperty]
        private bool isSelected;

        [ObservableProperty]
        private bool isMatched;

        [ObservableProperty]
        private bool isEnabled = true;

        [ObservableProperty]
        private bool isError;
    }

    public partial class ParovaciaOtazka : OtazkaBase
    {
        public ObservableCollection<ParovaciaPolozka> LavyStlpec { get; set; } = new();
        public ObservableCollection<ParovaciaPolozka> PravyStlpec { get; set; } = new();
        public ObservableCollection<ParovaciaPolicka> SpravnePary { get; set; } = new();

        [ObservableProperty]
        private ParovaciaPolozka? selectedLeft;

        [ObservableProperty]
        private ParovaciaPolozka? selectedRight;

        private int _matchedCount = 0;
        private int _errorCount = 0;
        private const int MaxErrors = 2;

        public IRelayCommand? SelectCommand { get; set; }

        public override bool SkontrolujOdpoved(object? odpoved)
        {
            return _matchedCount == SpravnePary.Count;
        }

        public void SelectItem(ParovaciaPolozka item)
        {
            if (!item.IsEnabled || item.IsMatched)
                return;

            if (item.IsLeft)
            {
                if (SelectedLeft != null)
                    SelectedLeft.IsSelected = false;

                SelectedLeft = item;
                item.IsSelected = true;

                if (SelectedRight != null)
                {
                    CheckMatch();
                }
            }
            else
            {
                if (SelectedRight != null)
                    SelectedRight.IsSelected = false;

                SelectedRight = item;
                item.IsSelected = true;

                if (SelectedLeft != null)
                {
                    CheckMatch();
                }
            }
        }

        private async void CheckMatch()
        {
            if (SelectedLeft == null || SelectedRight == null)
                return;

            var match = SpravnePary.FirstOrDefault(p => 
                p.Lava == SelectedLeft.Text && p.Prava == SelectedRight.Text);

            if (match != null && !match.IsMatched)
            {
                match.IsMatched = true;
                SelectedLeft.IsMatched = true;
                SelectedRight.IsMatched = true;
                SelectedLeft.IsEnabled = false;
                SelectedRight.IsEnabled = false;
                SelectedLeft.IsSelected = false;
                SelectedRight.IsSelected = false;

                _matchedCount++;

                if (_matchedCount == SpravnePary.Count)
                {
                    OdpovedCommand?.Execute(true);
                }
            }
            else
            {
                _errorCount++;
                
                // Show red error state
                var left = SelectedLeft;
                var right = SelectedRight;
                left.IsSelected = false;
                right.IsSelected = false;
                left.IsError = true;
                right.IsError = true;

                // Wait 500ms before removing error state
                await System.Threading.Tasks.Task.Delay(500);
                
                left.IsError = false;
                right.IsError = false;

                // Ak je 2 alebo viac chýb, pokračuj na ďalšiu otázku (nesprávna odpoveď)
                if (_errorCount >= MaxErrors)
                {
                    OdpovedCommand?.Execute(false);
                }
            }

            SelectedLeft = null;
            SelectedRight = null;
        }
    }
}
