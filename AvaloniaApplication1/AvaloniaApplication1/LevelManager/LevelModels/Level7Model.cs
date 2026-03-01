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
    public partial class Level7Model : ViewModelBase
    {
        private readonly MainViewModel _main;
        private readonly int _initialCount;
        private int _index = 0;
        private int _correctCount = 0;
        private int _answeredCount = 0;

        public Level7Model(MainViewModel main)
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
                    _main.Level7Completed = true;
                    _main.Level7Failed = false;
                }
                else
                {
                    _main.Level7Failed = true;
                }
                _main.ShowLevelyOverviewCommand.Execute(null);
            });

        private void NacitajOtazky()
        {
            // Načítaj dáta z JSON pomocou helper metódy
            var otazkyZJson = Data.QuestionConverter.ConvertToOtazky(7);

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
                OtazkaText = "Kedy sa používa cyklus for?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Keď nepoznáme počet opakovaní", Index=0 },
                    new ABCDMoznost{ Text="Keď poznáme presný počet opakovaní", Index=1 },
                    new ABCDMoznost{ Text="Keď chceme iba raz vykonať program", Index=2 },
                    new ABCDMoznost{ Text="Keď chceme použiť nekonečný cyklus", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 2 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Aká je správna syntax cyklu for?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="for { ZACIATOK; PODMIENKA; KROK }", Index=0 },
                    new ABCDMoznost{ Text="for (ZACIATOK; PODMIENKA; KROK) { ... }", Index=1 },
                    new ABCDMoznost{ Text="for ZACIATOK PODMIENKA KROK { ... }", Index=2 },
                    new ABCDMoznost{ Text="for (PODMIENKA) { ... }", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 3 - ABCD (s kódom)
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Aký bude výstup?\n\nfor(int i=0;i<=5;i=i+2){\nWriteLine(i);\n}",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="0 1 2 3 4 5", Index=0 },
                    new ABCDMoznost{ Text="0 2 4", Index=1 },
                    new ABCDMoznost{ Text="2 4 6", Index=2 },
                    new ABCDMoznost{ Text="0 2 4 6", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 4 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "V cykle for sa premenná cyklu zvyčajne označuje písmenom ____.",
                SpravnaOdpoved = "i"
            });

            // Otázka 5 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Aby sa cyklus for opakoval klesajúco, krok nastavíme na ____.",
                SpravnaOdpoved = "i--"
            });

            // Otázka 6 - Párovacia
            var parovaciaOtazka = new ParovaciaOtazka
            {
                OtazkaText = "Spojte časti cyklu for s ich významom"
            };

            // Ľavý stĺpec
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "ZACIATOK", Index = 0, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "PODMIENKA", Index = 1, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "KROK", Index = 2, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "Premenná cyklu", Index = 3, IsLeft = true });

            // Pravý stĺpec (premiešané poradie)
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "podmienka, kým sa cyklus opakuje", Index = 0, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "ako sa mení premenná po každom opakovaní", Index = 1, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "premenná, ktorá sa mení počas cyklu", Index = 2, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "vytvorenie a inicializácia premennej cyklu", Index = 3, IsLeft = false });

            // Správne páry
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "ZACIATOK", Prava = "vytvorenie a inicializácia premennej cyklu" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "PODMIENKA", Prava = "podmienka, kým sa cyklus opakuje" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "KROK", Prava = "ako sa mení premenná po každom opakovaní" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "Premenná cyklu", Prava = "premenná, ktorá sa mení počas cyklu" });

            Otazky.Add(parovaciaOtazka);
        }
    }
}
