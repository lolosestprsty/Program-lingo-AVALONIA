using Avalonia.Media;
using AvaloniaApplication1.LevelManager.Otazky;
using AvaloniaApplication1.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AvaloniaApplication1.LevelManager.LevelModels
{
    public partial class Level1Model : ViewModelBase
    {
        private readonly MainViewModel _main;
        private readonly int _initialCount;
        private int _index = 0;
        private int _correctCount = 0;
        private int _answeredCount = 0;
        public Level1Model(MainViewModel main)
        {
            _main = main;

            // nahraj z JSON
            NacitajOtazky();

            _initialCount = Otazky.Count;
            _index = 0;
            _correctCount = 0;
            OdpovedCommand = new RelayCommand<object>(odpoved =>
            {
                if (AktualnaOtazka is null)
                    return;

                bool spravna = AktualnaOtazka.SkontrolujOdpoved(odpoved);

                _answeredCount++;

                if (spravna)
                {
                    _correctCount++;
                    ProgressColor = Brushes.Green;
                }
                else
                {
                    ProgressColor = Brushes.Red;
                }

                Progres = (int)((double)_answeredCount / _initialCount * 100);

                // dalej button len pre zle odpovede (nie parovacie)
                ShowDalej = !spravna && !(AktualnaOtazka is ParovaciaOtazka);

                // spravna odpoved = dalsia otazka hned
                if (spravna)
                {
                    AwaitingNext = false;
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
                    ShowDalej = false;
                }
                else
                {
                    // cakat na dalej (okrem parovacie)
                    AwaitingNext = !(AktualnaOtazka is ParovaciaOtazka);

                    // parovacie pokracuju aj po zle
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

            // prirad command kazdej otazke
            foreach (var otazka in Otazky)
            {
                if (otazka is ABCDOtazka a)
                    a.OdpovedCommand = OdpovedCommand;
                else if (otazka is VstupnaOtazka v)
                    v.OdpovedCommand = OdpovedCommand;
            }

            // prva otazka
            AktualnaOtazka = Otazky.First();
        }

        public IRelayCommand BackCommand =>
            new RelayCommand(() =>
            {
                _main.ShowLevelyOverviewCommand.Execute(null);
            });


        public ObservableCollection<OtazkaBase> Otazky { get; } = new();

        [ObservableProperty]
        private OtazkaBase? aktualnaOtazka;

        [ObservableProperty]
        private int progres;

        [ObservableProperty]
        private Avalonia.Media.IBrush progressColor = Brushes.Green;

        [ObservableProperty]
        private int correctCount;

        [ObservableProperty]
        private bool isFinished;

        private bool _awaitingNext;
        public bool AwaitingNext { get => _awaitingNext; set => SetProperty(ref _awaitingNext, value); }

        private bool _showDalej;
        public bool ShowDalej { get => _showDalej; set => SetProperty(ref _showDalej, value); }

        public IRelayCommand DalsiaCommand =>
            new RelayCommand(() =>
            {
                // ked klikne dalej
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
            // vycisti flags pred dalsou otazkou
            if (q is ABCDOtazka a)
            {
                a.ShowCorrectAnswerFlag = false;
                a.SpravnaMoznostText = string.Empty;
            }
            else if (q is VstupnaOtazka v)
            {
            }
            else if (q is ParovaciaOtazka p)
            {
                // reset parovacich poloziek
                foreach (var it in p.LavyStlpec) { it.IsMatched = false; it.IsSelected = false; it.IsEnabled = true; }
                foreach (var it in p.PravyStlpec) { it.IsMatched = false; it.IsSelected = false; it.IsEnabled = true; }
            }
        }

        public int TotalQuestions => _initialCount;

        private void NacitajOtazky()
        {
            // nahraj z JSON
            var otazkyZJson = Data.QuestionConverter.ConvertToOtazky(1);

            foreach (var otazka in otazkyZJson)
            {
                Otazky.Add(otazka);
            }

            if (Otazky.Count == 0)
            {
                Console.WriteLine("WARNING: Level 1 - ziadne otazky!");
            }
        }
        public IRelayCommand<object> OdpovedCommand { get; set; }

        public IRelayCommand OkCommand =>
            new RelayCommand(() =>
            {
                // odomkni dalsi level ak >75%
                double percentageCorrect = (double)CorrectCount / _initialCount * 100;
                if (percentageCorrect >= 75)
                {
                    _main.Level1Completed = true;
                    _main.Level1Failed = false;
                }
                else
                {
                    _main.Level1Failed = true;
                }
                _main.ShowLevelyOverviewCommand.Execute(null);
            });

        private void DalsiaOtazka()
        {
            _index++;

            if (_index >= Otazky.Count)
            {
                _main.ShowLevelyOverviewCommand.Execute(null);
                return;
            }

            AktualnaOtazka = Otazky[_index];
        }
    }
}
