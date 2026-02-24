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
    public partial class Level3Model : ViewModelBase
    {
        private readonly MainViewModel _main;
        private readonly int _initialCount;
        private int _index = 0;
        private int _correctCount = 0;
        private int _answeredCount = 0;

        public Level3Model(MainViewModel main)
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
            {
                if (otazka is ABCDOtazka a)
                    a.OdpovedCommand = OdpovedCommand;
                else if (otazka is VstupnaOtazka v)
                    v.OdpovedCommand = OdpovedCommand;
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

        public IRelayCommand<object> OdpovedCommand { get; set; }

        public IRelayCommand OkCommand =>
            new RelayCommand(() =>
            {
                // unlock next level when 75% or more questions were answered correctly
                double percentageCorrect = (double)CorrectCount / _initialCount * 100;
                if (percentageCorrect >= 75)
                {
                    _main.Level3Completed = true;
                }
                _main.ShowLevelyOverviewCommand.Execute(null);
            });

        private void NacitajOtazky()
        {
            // Otázka 9 - Doplňovačka
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Každý riadok kódu ukončujeme znakom _______",
                SpravnaOdpoved = ";"
            });

            // Otázka 10 - Doplňovačka
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Na načítanie vstupu z klávesnice používame metódu __________",
                SpravnaOdpoved = "Console.ReadLine()"
            });

            // Otázka 11 - Doplňovačka
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Prevod vstupu na celé číslo urobíme pomocou __________",
                SpravnaOdpoved = "Convert.ToInt32"
            });

            // Otázka 12 - Doplňovačka
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Dátový typ pre jedno písmeno alebo znak je __________",
                SpravnaOdpoved = "char"
            });

            // Otázka 13 - Spoj dátový typ int s významom
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Spoj dátový typ 'int' s jeho významom:",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="celé číslo", Index=0 },
                    new ABCDMoznost{ Text="desatinné číslo", Index=1 },
                    new ABCDMoznost{ Text="jeden znak", Index=2 },
                    new ABCDMoznost{ Text="text", Index=3 },
                },
                SpravnaMoznostIndex = 0
            });

            // Otázka 14 - Spoj dátový typ double s významom
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Spoj dátový typ 'double' s jeho významom:",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="celé číslo", Index=0 },
                    new ABCDMoznost{ Text="desatinné číslo", Index=1 },
                    new ABCDMoznost{ Text="jeden znak", Index=2 },
                    new ABCDMoznost{ Text="text", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 15 - Spoj dátový typ char s významom
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Spoj dátový typ 'char' s jeho významom:",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="celé číslo", Index=0 },
                    new ABCDMoznost{ Text="desatinné číslo", Index=1 },
                    new ABCDMoznost{ Text="jeden znak", Index=2 },
                    new ABCDMoznost{ Text="text", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });

            // Otázka 16 - Spoj dátový typ string s významom
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Spoj dátový typ 'string' s jeho významom:",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="celé číslo", Index=0 },
                    new ABCDMoznost{ Text="desatinné číslo", Index=1 },
                    new ABCDMoznost{ Text="jeden znak", Index=2 },
                    new ABCDMoznost{ Text="text", Index=3 },
                },
                SpravnaMoznostIndex = 3
            });
        }
    }
}
