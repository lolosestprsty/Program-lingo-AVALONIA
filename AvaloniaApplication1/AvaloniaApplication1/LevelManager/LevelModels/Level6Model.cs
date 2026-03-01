using System;
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
    public partial class Level6Model : ViewModelBase
    {
        private readonly MainViewModel _main;
        private readonly int _initialCount;
        private int _index = 0;
        private int _correctCount = 0;
        private int _answeredCount = 0;

        public Level6Model(MainViewModel main)
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
                    _main.Level6Completed = true;
                    _main.Level6Failed = false;
                }
                else
                {
                    _main.Level6Failed = true;
                }
                _main.ShowLevelyOverviewCommand.Execute(null);
            });

        private void NacitajOtazky()
        {
            // Načítaj dáta z JSON pomocou helper metódy
            var otazkyZJson = Data.QuestionConverter.ConvertToOtazky(6);

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
                OtazkaText = "Na čo slúži príkaz switch v jazyku C#?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Na opakovanie cyklu", Index=0 },
                    new ABCDMoznost{ Text="Na výber z viacerých možností podľa hodnoty premennej", Index=1 },
                    new ABCDMoznost{ Text="Na deklaráciu premennej", Index=2 },
                    new ABCDMoznost{ Text="Na vytváranie tried", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 2 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Ktorý príkaz ukončuje vetvu case?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="stop", Index=0 },
                    new ABCDMoznost{ Text="return", Index=1 },
                    new ABCDMoznost{ Text="break", Index=2 },
                    new ABCDMoznost{ Text="end", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });

            // Otázka 3 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Čo sa vykoná, ak žiadny case nezodpovedá hodnote premennej?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Program sa ukončí", Index=0 },
                    new ABCDMoznost{ Text="Vykoná sa prvý case", Index=1 },
                    new ABCDMoznost{ Text="Vykoná sa vetva default", Index=2 },
                    new ABCDMoznost{ Text="Nič sa nestane", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });

            // Otázka 4 - BONUS ABCD (logika)
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Čo vypíše tento kód?\n\nint x = 3;\nswitch(x) {\ncase 1: WriteLine(\"A\"); break;\ncase 3: WriteLine(\"B\"); break;\ncase 5: WriteLine(\"C\"); break;\ndefault: WriteLine(\"D\"); break;\n}",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="A", Index=0 },
                    new ABCDMoznost{ Text="B", Index=1 },
                    new ABCDMoznost{ Text="C", Index=2 },
                    new ABCDMoznost{ Text="D", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 5 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Každá vetva case musí byť ukončená príkazom __________.",
                SpravnaOdpoved = "break"
            });

            // Otázka 6 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Vetva, ktorá sa vykoná, ak nenastane žiadna možnosť: __________",
                SpravnaOdpoved = "default"
            });

            // Otázka 7 - Párovacia (pojmy)
            var parovaciaOtazka1 = new ParovaciaOtazka
            {
                OtazkaText = "Spoj správne pojmy"
            };

            // Ľavý stĺpec
            parovaciaOtazka1.LavyStlpec.Add(new ParovaciaPolozka { Text = "switch", Index = 0, IsLeft = true });
            parovaciaOtazka1.LavyStlpec.Add(new ParovaciaPolozka { Text = "case", Index = 1, IsLeft = true });
            parovaciaOtazka1.LavyStlpec.Add(new ParovaciaPolozka { Text = "break", Index = 2, IsLeft = true });
            parovaciaOtazka1.LavyStlpec.Add(new ParovaciaPolozka { Text = "default", Index = 3, IsLeft = true });

            // Pravý stĺpec (premiešané poradie)
            parovaciaOtazka1.PravyStlpec.Add(new ParovaciaPolozka { Text = "ukončenie vetvy", Index = 0, IsLeft = false });
            parovaciaOtazka1.PravyStlpec.Add(new ParovaciaPolozka { Text = "vykoná sa pri nezhode", Index = 1, IsLeft = false });
            parovaciaOtazka1.PravyStlpec.Add(new ParovaciaPolozka { Text = "výber z viacerých možností", Index = 2, IsLeft = false });
            parovaciaOtazka1.PravyStlpec.Add(new ParovaciaPolozka { Text = "konkrétna hodnota", Index = 3, IsLeft = false });

            // Správne páry
            parovaciaOtazka1.SpravnePary.Add(new ParovaciaPolicka { Lava = "switch", Prava = "výber z viacerých možností" });
            parovaciaOtazka1.SpravnePary.Add(new ParovaciaPolicka { Lava = "case", Prava = "konkrétna hodnota" });
            parovaciaOtazka1.SpravnePary.Add(new ParovaciaPolicka { Lava = "break", Prava = "ukončenie vetvy" });
            parovaciaOtazka1.SpravnePary.Add(new ParovaciaPolicka { Lava = "default", Prava = "vykoná sa pri nezhode" });

            Otazky.Add(parovaciaOtazka1);

            // Otázka 8 - Párovacia (operácie)
            var parovaciaOtazka2 = new ParovaciaOtazka
            {
                OtazkaText = "Spoj operáciu s významom"
            };

            // Ľavý stĺpec
            parovaciaOtazka2.LavyStlpec.Add(new ParovaciaPolozka { Text = "case", Index = 0, IsLeft = true });
            parovaciaOtazka2.LavyStlpec.Add(new ParovaciaPolozka { Text = "break", Index = 1, IsLeft = true });
            parovaciaOtazka2.LavyStlpec.Add(new ParovaciaPolozka { Text = "default", Index = 2, IsLeft = true });
            parovaciaOtazka2.LavyStlpec.Add(new ParovaciaPolozka { Text = "switch", Index = 3, IsLeft = true });

            // Pravý stĺpec (premiešané poradie)
            parovaciaOtazka2.PravyStlpec.Add(new ParovaciaPolozka { Text = "rozhodovanie podľa hodnoty", Index = 0, IsLeft = false });
            parovaciaOtazka2.PravyStlpec.Add(new ParovaciaPolozka { Text = "vykoná sa ak žiadny case neplatí", Index = 1, IsLeft = false });
            parovaciaOtazka2.PravyStlpec.Add(new ParovaciaPolozka { Text = "vetva pre konkrétnu hodnotu", Index = 2, IsLeft = false });
            parovaciaOtazka2.PravyStlpec.Add(new ParovaciaPolozka { Text = "ukončí vykonávanie case", Index = 3, IsLeft = false });

            // Správne páry
            parovaciaOtazka2.SpravnePary.Add(new ParovaciaPolicka { Lava = "case", Prava = "vetva pre konkrétnu hodnotu" });
            parovaciaOtazka2.SpravnePary.Add(new ParovaciaPolicka { Lava = "break", Prava = "ukončí vykonávanie case" });
            parovaciaOtazka2.SpravnePary.Add(new ParovaciaPolicka { Lava = "default", Prava = "vykoná sa ak žiadny case neplatí" });
            parovaciaOtazka2.SpravnePary.Add(new ParovaciaPolicka { Lava = "switch", Prava = "rozhodovanie podľa hodnoty" });

            Otazky.Add(parovaciaOtazka2);
        }
    }
}
