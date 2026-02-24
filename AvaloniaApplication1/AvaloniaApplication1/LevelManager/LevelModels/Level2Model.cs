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
    public partial class Level2Model : ViewModelBase
    {
        private readonly MainViewModel _main;
        private readonly int _initialCount;
        private int _index = 0;
        private int _correctCount = 0;
        private int _answeredCount = 0;

        public Level2Model(MainViewModel main)
        {
            _main = main;

            NacitajOtazky();

            _initialCount = Otazky.Count;

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
                if (otazka is ABCDOtazka a)
                    a.OdpovedCommand = OdpovedCommand;

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

        public int TotalQuestions => _initialCount;

        public IRelayCommand<object> OdpovedCommand { get; set; }

        public IRelayCommand OkCommand =>
            new RelayCommand(() =>
            {
                // unlock next level when 75% or more questions were answered correctly
                double percentageCorrect = (double)CorrectCount / _initialCount * 100;
                if (percentageCorrect >= 75)
                {
                    _main.Level2Completed = true;
                    _main.Level2Failed = false;
                }
                else
                {
                    _main.Level2Failed = true;
                }
                _main.ShowLevelyOverviewCommand.Execute(null);
            });

        private void NacitajOtazky()
        {
            // Otázka 1
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Čím sa ukončuje každý riadok kódu v jazyku C#?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Bodkou", Index=0 },
                    new ABCDMoznost{ Text="Dvojbodkou", Index=1 },
                    new ABCDMoznost{ Text="Bodkočiarkou", Index=2 },
                    new ABCDMoznost{ Text="Čiarkou", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });

            // Otázka 2
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Na čo slúžia zložené zátvorky {} v jazyku C#?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Na oddelenie parametrov", Index=0 },
                    new ABCDMoznost{ Text="Na označenie bloku kódu", Index=1 },
                    new ABCDMoznost{ Text="Na ukončenie programu", Index=2 },
                    new ABCDMoznost{ Text="Na zapisovanie komentárov", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 3
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Čo znamená zápis Console.WriteLine(\"Hello\");",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Vytvorenie premennej", Index=0 },
                    new ABCDMoznost{ Text="Výpis textu do konzoly a prechod na nový riadok", Index=1 },
                    new ABCDMoznost{ Text="Načítanie vstupu", Index=2 },
                    new ABCDMoznost{ Text="Ukončenie programu", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 4
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Čo robí bodka medzi Console a WriteLine?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Oddeľuje dva riadky kódu", Index=0 },
                    new ABCDMoznost{ Text="Spája dva texty", Index=1 },
                    new ABCDMoznost{ Text="Umožňuje prístup k metóde triedy", Index=2 },
                    new ABCDMoznost{ Text="Ukončuje blok kódu", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });

            // Otázka 5
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Aký je rozdiel medzi Write() a WriteLine()?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Nie je žiadny rozdiel", Index=0 },
                    new ABCDMoznost{ Text="WriteLine vypíše text bez odriadkovania", Index=1 },
                    new ABCDMoznost{ Text="Write nevytvorí nový riadok, WriteLine áno", Index=2 },
                    new ABCDMoznost{ Text="Write slúži na vstup", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });

            // Otázka 6
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Ako zapisujeme jednoriadkový komentár?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="/* komentár */", Index=0 },
                    new ABCDMoznost{ Text="// komentár", Index=1 },
                    new ABCDMoznost{ Text="# komentár", Index=2 },
                    new ABCDMoznost{ Text="-- komentár", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 7
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Čo je premenná?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Funkcia na výpis", Index=0 },
                    new ABCDMoznost{ Text="Pomenované miesto v pamäti", Index=1 },
                    new ABCDMoznost{ Text="Typ komentára", Index=2 },
                    new ABCDMoznost{ Text="Operátor", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 8
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Ktorý dátový typ slúži na uloženie textu?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="int", Index=0 },
                    new ABCDMoznost{ Text="double", Index=1 },
                    new ABCDMoznost{ Text="string", Index=2 },
                    new ABCDMoznost{ Text="char", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });
        }
    }
}
