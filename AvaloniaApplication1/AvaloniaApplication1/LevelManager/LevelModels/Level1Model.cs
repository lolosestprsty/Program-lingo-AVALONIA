using Avalonia.Media;
using AvaloniaApplication1.LevelManager.Otazky;
using AvaloniaApplication1.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

            //nacitanie z JSON
            NacitajOtazky();

            // remember initial count to compute progress
            _initialCount = Otazky.Count;

            // initialize index and counts
            _index = 0;
            _correctCount = 0;

            //definovanie commandu
            OdpovedCommand = new RelayCommand<object>(odpoved =>
            {
                if (AktualnaOtazka is null)
                    return;

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

                // move to next question
                _index++;

                if (_index >= Otazky.Count)
                {
                    // finished - show summary
                    CorrectCount = _correctCount;
                    IsFinished = true;
                    return;
                }

                AktualnaOtazka = Otazky[_index];
            });

            //command ku kazdej otazke (set the command on question instances so views that use it directly can call it)
            foreach (var otazka in Otazky)
            {
                if (otazka is ABCDOtazka a)
                    a.OdpovedCommand = OdpovedCommand;
                else if (otazka is VstupnaOtazka v)
                    v.OdpovedCommand = OdpovedCommand;
            }

            //prva otazka je aktualna
            AktualnaOtazka = Otazky.First();
        }

        public IRelayCommand BackCommand =>
            new RelayCommand(() =>
            {
                _main.ShowLevelyOverviewCommand.Execute(null);
            });


        //otazky
        public ObservableCollection<OtazkaBase> Otazky { get; } = new();

        [ObservableProperty]
        private OtazkaBase? aktualnaOtazka;

        [ObservableProperty]
        private int progres; // 0–100

        [ObservableProperty]
        private Avalonia.Media.IBrush progressColor = Brushes.Green;

        [ObservableProperty]
        private int correctCount;

        [ObservableProperty]
        private bool isFinished;

        public int TotalQuestions => _initialCount;

        // index already declared above

        #region Nacitaj Otazky
        private void NacitajOtazky()
        {
            // Načítaj dáta z JSON pomocou helper metódy
            var otazkyZJson = Data.QuestionConverter.ConvertToOtazky(1);

            // Pridaj otázky do kolekcie
            foreach (var otazka in otazkyZJson)
            {
                Otazky.Add(otazka);
            }
        }
        #endregion
        public IRelayCommand<object> OdpovedCommand { get; set; }

        public IRelayCommand OkCommand =>
            new RelayCommand(() =>
            {
                // unlock next level when 75% or more questions were answered correctly
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
                // return to overview regardless
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
