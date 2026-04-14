using Avalonia.Media;
using AvaloniaApplication1.LevelManager.Otazky;
using AvaloniaApplication1.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.LevelManager.LevelModels
{
    public partial class Level9Model : ViewModelBase
    {
        private readonly MainViewModel _main;
        private readonly int _initialCount;
        private int _index = 0;
        private int _correctCount = 0;
        private int _answeredCount = 0;
        private bool _awaitingNext;
        private bool _showDalej;

        public Level9Model(MainViewModel main)
        {
            _main = main;

            NacitajOtazky();

            _initialCount = Otazky.Count;

            OdpovedCommand = new RelayCommand<object>(odpoved =>
            {
                AwaitingNext = false;
                if (AktualnaOtazka is null)
                    return;

                // Validate empty input for VstupnaOtazka
                if (AktualnaOtazka is VstupnaOtazka && odpoved is string textInput && string.IsNullOrWhiteSpace(textInput))
                {
                    Console.WriteLine("vypln textove pole");
                    return;
                }

                bool spravna = AktualnaOtazka.SkontrolujOdpoved(odpoved);

                _answeredCount++;

                if (spravna)
                {
                    CorrectCount++;
                    ProgressColor = Brushes.Green;
                }
                else
                {
                    ProgressColor = Brushes.Red;
                }

                Progres = (int)((double)_answeredCount / _initialCount * 100);

                _index++;

                if (_index >= Otazky.Count)
                {
                    CorrectCount = _correctCount;
                    IsFinished = true;
                    return;
                }

                AktualnaOtazka = Otazky[_index];
            });

            foreach (var otazka in Otazky)
            {
                if (otazka is ABCDOtazka a)
                    a.OdpovedCommand = OdpovedCommand;
                else if (otazka is VstupnaOtazka v)
                    v.OdpovedCommand = OdpovedCommand;
                else if (otazka is ParovaciaOtazka p)
                {
                    p.OdpovedCommand = OdpovedCommand;
                    p.SelectCommand = new RelayCommand<ParovaciaPolozka>(item => p.SelectItem(item!));
                }
            }

            AktualnaOtazka = Otazky.First();
        }

        public IRelayCommand BackCommand =>
            new RelayCommand(() =>
            {
                _main.ShowLevelyOverviewCommand.Execute(null);
            });

        public ObservableCollection<OtazkaBase> Otazky { get; } = new();

        [ObservableProperty] private OtazkaBase? aktualnaOtazka;
        [ObservableProperty] private int progres;
        [ObservableProperty] private IBrush progressColor = Brushes.Green;
        [ObservableProperty] private int correctCount;
        [ObservableProperty] private bool isFinished;
        [ObservableProperty] private bool showDalej;
        [ObservableProperty] private bool awaitingNext;

        public IRelayCommand DalsiaCommand =>
            new RelayCommand(() =>
            {
                // advance to next question after user clicked 'Dalej'
                AwaitingNext = false;

                _index++;

                if (_index >= Otazky.Count)
                {
                    IsFinished = true;
                    return;
                }

                var next = Otazky[_index];
                ResetQuestionFlags(next);
                AktualnaOtazka = next;
            });

        private void ResetQuestionFlags(OtazkaBase q)
        {
            // clear answer visibility flags when moving to a next question
            if (q is ABCDOtazka a)
            {
                a.ShowCorrectAnswerFlag = false;
                // reset the displayed correct answer text
                a.SpravnaMoznostText = string.Empty;
            }
            else if (q is VstupnaOtazka v)
            {
                v.ShowCorrectAnswerFlag = false;
            }
            else if (q is ParovaciaOtazka p)
            {
                // reset parovacia items
                foreach (var it in p.LavyStlpec) { it.IsMatched = false; it.IsSelected = false; it.IsEnabled = true; }
                foreach (var it in p.PravyStlpec) { it.IsMatched = false; it.IsSelected = false; it.IsEnabled = true; }
            }
        }

        public int TotalQuestions => _initialCount;

        public IRelayCommand<object> OdpovedCommand { get; set; }

        public IRelayCommand OkCommand =>
            new RelayCommand(() =>
            {
                // unlock next level when 75% or more questions were answered correctly
                double percentageCorrect = (double)CorrectCount / _initialCount * 100;
                if (percentageCorrect >= 75)
                {
                    _main.Level9Completed = true;
                    _main.Level9Failed = false;
                }
                else
                {
                    _main.Level9Failed = true;
                }
                _main.ShowLevelyOverviewCommand.Execute(null);
            });

        private void NacitajOtazky()
        {
            // Nacítaj dáta z JSON
            var otazkyZJson = Data.QuestionConverter.ConvertToOtazky(9);

            foreach (var otazka in otazkyZJson)
            {
                Otazky.Add(otazka);
            }

            // Ak sa nenacítali žiadne otázky z databázy, môže to znamenat problém
            if (Otazky.Count == 0)
            {
                Console.WriteLine("WARNING: Level 9 - žiadne otázky neboli nacítané z databázy!");
            }
        }
    }
}
