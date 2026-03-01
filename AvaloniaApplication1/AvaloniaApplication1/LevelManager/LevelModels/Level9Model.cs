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

        public Level9Model(MainViewModel main)
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
            // Načítaj dáta z JSON pomocou helper metódy
            var otazkyZJson = Data.QuestionConverter.ConvertToOtazky(9);

            if (otazkyZJson.Count > 0)
            {
                foreach (var otazka in otazkyZJson)
                {
                    Otazky.Add(otazka);
                }
                return;
            }

            // Fallback: hardcoded otázky
            // Otázka 1 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Na čo slúži trieda Random?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Na opakovanie cyklu", Index=0 },
                    new ABCDMoznost{ Text="Na generovanie náhodných čísel", Index=1 },
                    new ABCDMoznost{ Text="Na ukladanie údajov", Index=2 },
                    new ABCDMoznost{ Text="Na vytváranie tried", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 2 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Čo môže vrátiť výraz:\n\nrand.Next(1, 6);",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="1 až 6", Index=0 },
                    new ABCDMoznost{ Text="1 až 5", Index=1 },
                    new ABCDMoznost{ Text="0 až 6", Index=2 },
                    new ABCDMoznost{ Text="0 až 5", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 3 - ABCD (logická)
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Prečo nie je vhodné vytvárať new Random() v každom cykle?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Spôsobí chybu kompilácie", Index=0 },
                    new ABCDMoznost{ Text="Generuje rovnaké čísla", Index=1 },
                    new ABCDMoznost{ Text="Spomaľuje počítač", Index=2 },
                    new ABCDMoznost{ Text="Neexistuje dôvod", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 4 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Doplň názov metódy, ktorá generuje desatinné číslo od 0 po menej ako 1:\n\nrand.__________();",
                SpravnaOdpoved = "NextDouble"
            });

            // Otázka 5 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Horná hranica v metóde Next(min, max) je vždy __________ (menšia / väčšia / rovná).",
                SpravnaOdpoved = "menšia"
            });

            // Otázka 6 - Párovacia
            var parovaciaOtazka = new ParovaciaOtazka
            {
                OtazkaText = "Spoj správne dvojice - Metóda a jej rozsah"
            };

            // Ľavý stĺpec
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "Next()", Index = 0, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "Next(100)", Index = 1, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "Next(10,100)", Index = 2, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "NextDouble()", Index = 3, IsLeft = true });

            // Pravý stĺpec (premiešané poradie)
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "10 – 99", Index = 0, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "0.0 – menej ako 1.0", Index = 1, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "0 – 99", Index = 2, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "0 až int.MaxValue", Index = 3, IsLeft = false });

            // Správne páry
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "Next()", Prava = "0 až int.MaxValue" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "Next(100)", Prava = "0 – 99" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "Next(10,100)", Prava = "10 – 99" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "NextDouble()", Prava = "0.0 – menej ako 1.0" });

            Otazky.Add(parovaciaOtazka);
        }
    }
}
