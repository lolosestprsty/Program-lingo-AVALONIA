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
    public partial class Level10Model : ViewModelBase
    {
        private readonly MainViewModel _main;
        private readonly int _initialCount;
        private int _index = 0;
        private int _correctCount = 0;
        private int _answeredCount = 0;

        public Level10Model(MainViewModel main)
        {
            _main = main;

            NacitajOtazky();

            _initialCount = Otazky.Count;

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

        public int TotalQuestions => _initialCount;

        public IRelayCommand<object> OdpovedCommand { get; set; }

        public IRelayCommand OkCommand =>
            new RelayCommand(() =>
            {
                // unlock next level when 75% or more questions were answered correctly
                double percentageCorrect = (double)CorrectCount / _initialCount * 100;
                if (percentageCorrect >= 75)
                {
                    _main.Level10Completed = true;
                    _main.Level10Failed = false;
                }
                else
                {
                    _main.Level10Failed = true;
                }
                _main.ShowLevelyOverviewCommand.Execute(null);
            });

        private void NacitajOtazky()
        {
            // Otázka 1 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Aký index má prvý prvok poľa v C#?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="1", Index=0 },
                    new ABCDMoznost{ Text="-1", Index=1 },
                    new ABCDMoznost{ Text="0", Index=2 },
                    new ABCDMoznost{ Text="Závisí od veľkosti poľa", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });

            // Otázka 2 - ABCD (s kódom)
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Čo vypíše tento kód?\n\nint[] cisla = {10, 20, 30};\nWriteLine(cisla[1]);",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="10", Index=0 },
                    new ABCDMoznost{ Text="20", Index=1 },
                    new ABCDMoznost{ Text="30", Index=2 },
                    new ABCDMoznost{ Text="Chybu", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 3 - ABCD (foreach)
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Čo je hlavný rozdiel medzi for a foreach?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="foreach je rýchlejší", Index=0 },
                    new ABCDMoznost{ Text="foreach nemôže meniť index", Index=1 },
                    new ABCDMoznost{ Text="foreach sa používa na prechádzanie kolekcií bez indexu", Index=2 },
                    new ABCDMoznost{ Text="neexistuje rozdiel", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });

            // Otázka 4 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Doplň názov vlastnosti, ktorá vracia počet prvkov poľa:\n\npole.__________",
                SpravnaOdpoved = "Length"
            });

            // Otázka 5 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Ak sa pokúsime použiť index mimo rozsahu poľa, vznikne chyba typu: __________",
                SpravnaOdpoved = "IndexOutOfRangeException"
            });

            // Otázka 6 - Párovacia
            var parovaciaOtazka = new ParovaciaOtazka
            {
                OtazkaText = "Spoj správne dvojice"
            };

            // Ľavý stĺpec
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "index", Index = 0, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "Length", Index = 1, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "Array.Sort()", Index = 2, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "foreach", Index = 3, IsLeft = true });

            // Pravý stĺpec (premiešané poradie)
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "prechádzanie prvkov", Index = 0, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "poradie prvku", Index = 1, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "zoradenie poľa", Index = 2, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "počet prvkov", Index = 3, IsLeft = false });

            // Správne páry
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "index", Prava = "poradie prvku" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "Length", Prava = "počet prvkov" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "Array.Sort()", Prava = "zoradenie poľa" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "foreach", Prava = "prechádzanie prvkov" });

            Otazky.Add(parovaciaOtazka);
        }
    }
}
