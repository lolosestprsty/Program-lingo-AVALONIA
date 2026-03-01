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
    public partial class Level8Model : ViewModelBase
    {
        private readonly MainViewModel _main;
        private readonly int _initialCount;
        private int _index = 0;
        private int _correctCount = 0;
        private int _answeredCount = 0;

        public Level8Model(MainViewModel main)
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
                    _main.Level8Completed = true;
                    _main.Level8Failed = false;
                }
                else
                {
                    _main.Level8Failed = true;
                }
                _main.ShowLevelyOverviewCommand.Execute(null);
            });

        private void NacitajOtazky()
        {
            // Načítaj dáta z JSON pomocou helper metódy
            var otazkyZJson = Data.QuestionConverter.ConvertToOtazky(8);

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
                OtazkaText = "Kedy je vhodné použiť cyklus while namiesto for?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Keď poznáme presný počet opakovaní", Index=0 },
                    new ABCDMoznost{ Text="Keď nepoznáme presný počet opakovaní", Index=1 },
                    new ABCDMoznost{ Text="Keď chceme vykonať príkaz aspoň raz", Index=2 },
                    new ABCDMoznost{ Text="Keď chceme vypísať iba jednu hodnotu", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 2 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Čo sa stane pri cykle do…while, ak podmienka na začiatku nie je splnená?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Program sa nevykoná ani raz", Index=0 },
                    new ABCDMoznost{ Text="Program sa vykoná aspoň raz", Index=1 },
                    new ABCDMoznost{ Text="Program spôsobí nekonečný cyklus", Index=2 },
                    new ABCDMoznost{ Text="Program vyhodí chybu", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 3 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Aký je rozdiel medzi príkazmi break a continue?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="break preskočí jednu iteráciu, continue ukončí cyklus", Index=0 },
                    new ABCDMoznost{ Text="break ukončí cyklus, continue preskočí jednu iteráciu", Index=1 },
                    new ABCDMoznost{ Text="oba preskakujú jednu iteráciu", Index=2 },
                    new ABCDMoznost{ Text="oba ukončujú cyklus", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 4 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Nekonečný cyklus pri while môže vzniknúť, ak sa ______________ v podmienke nemení.",
                SpravnaOdpoved = "premenná"
            });

            // Otázka 5 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Príkaz continue spôsobí, že aktuálna iterácia cyklu sa ______________ a cyklus pokračuje ďalšou iteráciou.",
                SpravnaOdpoved = "preskočí"
            });

            // Otázka 6 - Párovacia
            var parovaciaOtazka = new ParovaciaOtazka
            {
                OtazkaText = "Spojte typ cyklu s jeho vlastnosťou"
            };

            // Ľavý stĺpec
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "for", Index = 0, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "while", Index = 1, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "do…while", Index = 2, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "nekonečný cyklus", Index = 3, IsLeft = true });

            // Pravý stĺpec (premiešané poradie)
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "vykoná príkazy len ak je podmienka splnená", Index = 0, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "nemá koniec, ak sa podmienka nemení", Index = 1, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "vykoná príkazy aspoň raz, potom kontroluje podmienku", Index = 2, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "používa sa pri známom počte opakovaní", Index = 3, IsLeft = false });

            // Správne páry
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "for", Prava = "používa sa pri známom počte opakovaní" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "while", Prava = "vykoná príkazy len ak je podmienka splnená" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "do…while", Prava = "vykoná príkazy aspoň raz, potom kontroluje podmienku" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "nekonečný cyklus", Prava = "nemá koniec, ak sa podmienka nemení" });

            Otazky.Add(parovaciaOtazka);
        }
    }
}
