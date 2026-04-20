using Avalonia.Media;
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
    public partial class Level4Model : ViewModelBase
    {
        private readonly MainViewModel _main;
        private readonly int _initialCount;
        private int _index = 0;
        private int _correctCount = 0;
        private int _answeredCount = 0;
        private bool _awaitingNext;
        private bool _showDalej;

        public Level4Model(MainViewModel main)
        {
            _main = main;

            NacitajOtazky();

            _initialCount = Otazky.Count;

            _index = 0;

            // define command: evaluate answer but wait for user to click "Dalej" to continue
            OdpovedCommand = new RelayCommand<object>(odpoved =>
            {
                if (AktualnaOtazka is null)
                    return;

                // Validate empty input for VstupnaOtazka
                if (AktualnaOtazka is VstupnaOtazka && odpoved is string textInput && string.IsNullOrWhiteSpace(textInput))
                {
                    Console.WriteLine("vypln textove pole");
                    return;
                }

                bool spravna = AktualnaOtazka.SkontrolujOdpoved(odpoved);

                // count every answered question
                _answeredCount++;

                if (spravna)
                {
                    _correctCount++;
                    ProgressColor = Brushes.Green;
                }
                else
                {
                    // mark progress color red for wrong answer
                    ProgressColor = Brushes.Red;
                }

                // update progress (based on how many questions were answered so far)
                Progres = (int)((double)_answeredCount / _initialCount * 100);

                // ensure 'Dalej' button only when incorrect (but not for ParovaciaOtazka)
                ShowDalej = !spravna && !(AktualnaOtazka is ParovaciaOtazka);

                // if correct -> auto-advance to next question
                if (spravna)
                {
                    // advance immediately
                    AwaitingNext = false;
                    _index++;

                    if (_index >= Otazky.Count)
                    {
                        // finished - show summary
                        CorrectCount = _correctCount;
                        IsFinished = true;
                        return;
                    }

                    // move to next without showing 'Dalej'
                    var next = Otazky[_index];
                    ResetQuestionFlags(next);
                    AktualnaOtazka = next;
                    // hide 'Dalej' because next is displayed
                    ShowDalej = false;
                }
                else
                {
                    // wait for user to press 'Dalej' before advancing (except ParovaciaOtazka)
                    AwaitingNext = !(AktualnaOtazka is ParovaciaOtazka);

                    // for ParovaciaOtazka, auto-advance even on wrong answer
                    if (AktualnaOtazka is ParovaciaOtazka)
                    {
                        _index++;

                        if (_index >= Otazky.Count)
                        {
                            CorrectCount = _correctCount;
                            IsFinished = true;
                            return;
                        }

                        var next = Otazky[_index];
                        ResetQuestionFlags(next);
                        AktualnaOtazka = next;
                    }
                }
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

        // explicit properties for AwaitingNext and ShowDalej (use backing fields declared above)
        public bool AwaitingNext { get => _awaitingNext; set => SetProperty(ref _awaitingNext, value); }

        public bool ShowDalej { get => _showDalej; set => SetProperty(ref _showDalej, value); }

        public IRelayCommand DalsiaCommand =>
            new RelayCommand(() =>
            {
                // advance to next question after user clicked 'Dalej'
                AwaitingNext = false;
                ShowDalej = false;

                _index++;

                if (_index >= Otazky.Count)
                {
                    CorrectCount = _correctCount;
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
                    _main.Level4Completed = true;
                    _main.Level4Failed = false;
                }
                else
                {
                    _main.Level4Failed = true;
                }
                _main.ShowLevelyOverviewCommand.Execute(null);
            });

        private void NacitajOtazky()
        {
            // Na?ítaj dáta z JSON
            var otazkyZJson = Data.QuestionConverter.ConvertToOtazky(4);

            foreach (var otazka in otazkyZJson)
            {
                Otazky.Add(otazka);
            }

            // Ak sa nena?ítali žiadne otázky z databázy, môže to znamena? problém
            if (Otazky.Count == 0)
            {
                Console.WriteLine("WARNING: Level 4 - žiadne otázky neboli na?ítané z databázy!");
            }
        }
    }
}
