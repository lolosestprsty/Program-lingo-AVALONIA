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
    public partial class Level4Model : ViewModelBase
    {
        private readonly MainViewModel _main;
        private readonly int _initialCount;
        private int _index = 0;
        private int _correctCount = 0;
        private int _answeredCount = 0;

        public Level4Model(MainViewModel main)
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
                    _main.Level4Completed = true;
                    _main.Level4Failed = false;
                }
                else
                {
                    _main.Level4Failed = true;
                }
                _main.ShowLevelyOverviewCommand.Execute(null);
            });

        private void NacitajOtazky()
        {
            // Otázka 1 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "?o je implicitná konverzia?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Manuálne pretypovanie", Index=0 },
                    new ABCDMoznost{ Text="Automatická konverzia bez straty údajov", Index=1 },
                    new ABCDMoznost{ Text="Prevod stringu na bool", Index=2 },
                    new ABCDMoznost{ Text="Chyba programu", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 2 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "?o sa stane pri (int)3.14?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Výsledok je 3.14", Index=0 },
                    new ABCDMoznost{ Text="Výsledok je 4", Index=1 },
                    new ABCDMoznost{ Text="Výsledok je 3", Index=2 },
                    new ABCDMoznost{ Text="Chyba", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });

            // Otázka 3 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Ktorý príkaz prevedie int na string?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Convert.ToDouble()", Index=0 },
                    new ABCDMoznost{ Text="Convert.ToString()", Index=1 },
                    new ABCDMoznost{ Text="Convert.ToBool()", Index=2 },
                    new ABCDMoznost{ Text="Convert.Int()", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 4 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Pre?o používame CultureInfo(\"en-US\")?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Na zrýchlenie programu", Index=0 },
                    new ABCDMoznost{ Text="Na zmenu jazyka aplikácie", Index=1 },
                    new ABCDMoznost{ Text="Na nastavenie bodky ako odde?ova?a desatinných miest", Index=2 },
                    new ABCDMoznost{ Text="Na výpis dátumu", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });

            // Otázka 5 - Dopl?ova?ka
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Manuálne pretypovanie zapisujeme pomocou __________ zátvoriek.",
                SpravnaOdpoved = "okrúhlych"
            });

            // Otázka 6 - Dopl?ova?ka
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Pri explicitnej konverzii môže dôjs? k strate __________.",
                SpravnaOdpoved = "údajov"
            });

            // Otázka 7 - Dopl?ova?ka
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "V zdrojovom kóde sa ako odde?ova? desatinných miest vždy používa __________.",
                SpravnaOdpoved = "bodka"
            });

            // Otázka 8 - Spoj dvojice
            var parovaciaOtazka = new ParovaciaOtazka
            {
                OtazkaText = "Spoj správne dvojice"
            };

            // ?avý st?pec
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "Implicitná konverzia", Index = 0, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "Explicitná konverzia", Index = 1, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "Convert.ToInt32", Index = 2, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "CultureInfo(\"en-US\")", Index = 3, IsLeft = true });

            // Pravý st?pec (premiešané poradie)
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "manuálne pretypovanie", Index = 0, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "bodka ako odde?ova?", Index = 1, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "bez straty údajov", Index = 2, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "prevod na int", Index = 3, IsLeft = false });

            // Správne páry
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "Implicitná konverzia", Prava = "bez straty údajov" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "Explicitná konverzia", Prava = "manuálne pretypovanie" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "Convert.ToInt32", Prava = "prevod na int" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "CultureInfo(\"en-US\")", Prava = "bodka ako odde?ova?" });

            Otazky.Add(parovaciaOtazka);
        }
    }
}
