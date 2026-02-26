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
    public partial class Level5Model : ViewModelBase
    {
        private readonly MainViewModel _main;
        private readonly int _initialCount;
        private int _index = 0;
        private int _correctCount = 0;
        private int _answeredCount = 0;

        public Level5Model(MainViewModel main)
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
                    _main.Level5Completed = true;
                    _main.Level5Failed = false;
                }
                else
                {
                    _main.Level5Failed = true;
                }
                _main.ShowLevelyOverviewCommand.Execute(null);
            });

        private void NacitajOtazky()
        {
            // Načítaj dáta z JSON pomocou helper metódy
            var otazkyZJson = Data.QuestionConverter.ConvertToOtazky(5);

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
                OtazkaText = "Na čo slúži príkaz if?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Na opakovanie cyklu", Index=0 },
                    new ABCDMoznost{ Text="Na rozhodovanie podľa podmienky", Index=1 },
                    new ABCDMoznost{ Text="Na vytvorenie premennej", Index=2 },
                    new ABCDMoznost{ Text="Na ukončenie programu", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 2 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Aký je rozdiel medzi = a == ?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Nie je žiadny rozdiel", Index=0 },
                    new ABCDMoznost{ Text="= porovnáva, == priraďuje", Index=1 },
                    new ABCDMoznost{ Text="= priraďuje hodnotu, == porovnáva", Index=2 },
                    new ABCDMoznost{ Text="Oba ukončujú príkaz", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });

            // Otázka 3 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Čo znamená operátor && ?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Aspoň jedna podmienka platí", Index=0 },
                    new ABCDMoznost{ Text="Obe podmienky musia platiť", Index=1 },
                    new ABCDMoznost{ Text="Negácia podmienky", Index=2 },
                    new ABCDMoznost{ Text="Sčítanie", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 4 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Koľko vetiev else môže byť v jednej podmienke?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Neobmedzene", Index=0 },
                    new ABCDMoznost{ Text="2", Index=1 },
                    new ABCDMoznost{ Text="1", Index=2 },
                    new ABCDMoznost{ Text="0", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });

            // Otázka 5 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Operátor na porovnanie rovnosti je _______",
                SpravnaOdpoved = "=="
            });

            // Otázka 6 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Operátor OR zapisujeme ako _______",
                SpravnaOdpoved = "||"
            });

            // Otázka 7 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Vetva, ktorá sa vykoná ak podmienka nie je splnená, sa nazýva _______",
                SpravnaOdpoved = "else"
            });

            // Otázka 8 - Párovacia
            var parovaciaOtazka = new ParovaciaOtazka
            {
                OtazkaText = "Spoj správne dvojice"
            };

            // Ľavý stĺpec
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "if", Index = 0, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "else", Index = 1, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "else if", Index = 2, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "&&", Index = 3, IsLeft = true });

            // Pravý stĺpec (premiešané poradie)
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "AND operátor", Index = 0, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "základná podmienka", Index = 1, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "vykoná sa ak podmienka neplatí", Index = 2, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "ďalšia možnosť rozhodovania", Index = 3, IsLeft = false });

            // Správne páry
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "if", Prava = "základná podmienka" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "else", Prava = "vykoná sa ak podmienka neplatí" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "else if", Prava = "ďalšia možnosť rozhodovania" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "&&", Prava = "AND operátor" });

            Otazky.Add(parovaciaOtazka);
        }
    }
}
