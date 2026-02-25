using AvaloniaApplication1.LevelManager.Otazky;
using AvaloniaApplication1.ViewModels;
using Avalonia.Media;
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
    public partial class Level12Model : ViewModelBase
    {
        private readonly MainViewModel _main;
        private readonly int _initialCount;
        private int _index = 0;
        private int _correctCount = 0;
        private int _answeredCount = 0;

        public Level12Model(MainViewModel main)
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
                    _main.Level12Completed = true;
                    _main.Level12Failed = false;
                }
                else
                {
                    _main.Level12Failed = true;
                }
                _main.ShowLevelyOverviewCommand.Execute(null);
            });

        private void NacitajOtazky()
        {
            // Otázka 1 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Čo je štruktúra (struct) v C#?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Pole rovnakých hodnôt", Index=0 },
                    new ABCDMoznost{ Text="Používateľom definovaný dátový typ združujúci viac údajov", Index=1 },
                    new ABCDMoznost{ Text="Metóda bez návratovej hodnoty", Index=2 },
                    new ABCDMoznost{ Text="Cyklus na opakovanie príkazov", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 2 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Ako pristupujeme k členom štruktúry?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Pomocou hranatých zátvoriek []", Index=0 },
                    new ABCDMoznost{ Text="Pomocou operátora ::", Index=1 },
                    new ABCDMoznost{ Text="Pomocou bodky .", Index=2 },
                    new ABCDMoznost{ Text="Pomocou kľúčového slova public", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });

            // Otázka 3 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Kam patrí struct z hľadiska typov?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Referenčný typ", Index=0 },
                    new ABCDMoznost{ Text="Hodnotový typ", Index=1 },
                    new ABCDMoznost{ Text="Dynamický typ", Index=2 },
                    new ABCDMoznost{ Text="Neznámy typ", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 4 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Doplň správne kľúčové slovo:\n\n________ Ziak\n{\n    public string meno;\n    public int vek;\n}",
                SpravnaOdpoved = "struct"
            });

            // Otázka 5 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Doplň vytvorenie premennej typu Ziak:\n\nZiak z1 = new ________();",
                SpravnaOdpoved = "Ziak"
            });

            // Otázka 6 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Doplň chýbajúcu časť (prístup k členu štruktúry):\n\nZiak z1 = new Ziak();\nz1._______ = \"Adam\";",
                SpravnaOdpoved = "meno"
            });

            // Otázka 7 - Párovacia (Pojem - Význam)
            var parovaciaOtazka1 = new ParovaciaOtazka
            {
                OtazkaText = "Spoj pojem s jeho významom"
            };

            // Ľavý stĺpec
            parovaciaOtazka1.LavyStlpec.Add(new ParovaciaPolozka { Text = "struct", Index = 0, IsLeft = true });
            parovaciaOtazka1.LavyStlpec.Add(new ParovaciaPolozka { Text = "člen štruktúry", Index = 1, IsLeft = true });
            parovaciaOtazka1.LavyStlpec.Add(new ParovaciaPolozka { Text = "Ziak[] ziaci", Index = 2, IsLeft = true });
            parovaciaOtazka1.LavyStlpec.Add(new ParovaciaPolozka { Text = "ref", Index = 3, IsLeft = true });

            // Pravý stĺpec (premiešané poradie - nie vedľa seba)
            parovaciaOtazka1.PravyStlpec.Add(new ParovaciaPolozka { Text = "Pole prvkov rovnakého typu", Index = 0, IsLeft = false });
            parovaciaOtazka1.PravyStlpec.Add(new ParovaciaPolozka { Text = "Odovzdanie premennej referenciou", Index = 1, IsLeft = false });
            parovaciaOtazka1.PravyStlpec.Add(new ParovaciaPolozka { Text = "Používateľom definovaný dátový typ", Index = 2, IsLeft = false });
            parovaciaOtazka1.PravyStlpec.Add(new ParovaciaPolozka { Text = "Premenná vo vnútri štruktúry", Index = 3, IsLeft = false });

            // Správne páry
            parovaciaOtazka1.SpravnePary.Add(new ParovaciaPolicka { Lava = "struct", Prava = "Používateľom definovaný dátový typ" });
            parovaciaOtazka1.SpravnePary.Add(new ParovaciaPolicka { Lava = "člen štruktúry", Prava = "Premenná vo vnútri štruktúry" });
            parovaciaOtazka1.SpravnePary.Add(new ParovaciaPolicka { Lava = "Ziak[] ziaci", Prava = "Pole prvkov rovnakého typu" });
            parovaciaOtazka1.SpravnePary.Add(new ParovaciaPolicka { Lava = "ref", Prava = "Odovzdanie premennej referenciou" });

            Otazky.Add(parovaciaOtazka1);

            // Otázka 8 - Párovacia (Kód - Význam)
            var parovaciaOtazka2 = new ParovaciaOtazka
            {
                OtazkaText = "Spoj kód s významom"
            };

            // Ľavý stĺpec
            parovaciaOtazka2.LavyStlpec.Add(new ParovaciaPolozka { Text = "ziaci[0].meno", Index = 0, IsLeft = true });
            parovaciaOtazka2.LavyStlpec.Add(new ParovaciaPolozka { Text = "Ziak[] ziaci = new Ziak[5];", Index = 1, IsLeft = true });
            parovaciaOtazka2.LavyStlpec.Add(new ParovaciaPolozka { Text = "sucet += ziaci[i].vyska;", Index = 2, IsLeft = true });
            parovaciaOtazka2.LavyStlpec.Add(new ParovaciaPolozka { Text = "ziaci.Length", Index = 3, IsLeft = true });

            // Pravý stĺpec (premiešané poradie - nie vedľa seba)
            parovaciaOtazka2.PravyStlpec.Add(new ParovaciaPolozka { Text = "Sčítanie výšky žiakov", Index = 0, IsLeft = false });
            parovaciaOtazka2.PravyStlpec.Add(new ParovaciaPolozka { Text = "Počet prvkov v poli", Index = 1, IsLeft = false });
            parovaciaOtazka2.PravyStlpec.Add(new ParovaciaPolozka { Text = "Prístup k menu prvého žiaka", Index = 2, IsLeft = false });
            parovaciaOtazka2.PravyStlpec.Add(new ParovaciaPolozka { Text = "Vytvorenie poľa štruktúr", Index = 3, IsLeft = false });

            // Správne páry
            parovaciaOtazka2.SpravnePary.Add(new ParovaciaPolicka { Lava = "ziaci[0].meno", Prava = "Prístup k menu prvého žiaka" });
            parovaciaOtazka2.SpravnePary.Add(new ParovaciaPolicka { Lava = "Ziak[] ziaci = new Ziak[5];", Prava = "Vytvorenie poľa štruktúr" });
            parovaciaOtazka2.SpravnePary.Add(new ParovaciaPolicka { Lava = "sucet += ziaci[i].vyska;", Prava = "Sčítanie výšky žiakov" });
            parovaciaOtazka2.SpravnePary.Add(new ParovaciaPolicka { Lava = "ziaci.Length", Prava = "Počet prvkov v poli" });

            Otazky.Add(parovaciaOtazka2);
        }
    }
}
