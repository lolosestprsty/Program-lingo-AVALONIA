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
    public partial class Level11Model : ViewModelBase
    {
        private readonly MainViewModel _main;
        private readonly int _initialCount;
        private int _index = 0;
        private int _correctCount = 0;
        private int _answeredCount = 0;

        public Level11Model(MainViewModel main)
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
                    _main.Level11Completed = true;
                    _main.Level11Failed = false;
                }
                else
                {
                    _main.Level11Failed = true;
                }
                _main.ShowLevelyOverviewCommand.Execute(null);
            });

        private void NacitajOtazky()
        {
            // Otázka 1 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Čo je dvojrozmerné pole?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Premenná, ktorá obsahuje len jedno číslo", Index=0 },
                    new ABCDMoznost{ Text="Pole, ktoré obsahuje iba textové hodnoty", Index=1 },
                    new ABCDMoznost{ Text="Pole usporiadané do riadkov a stĺpcov", Index=2 },
                    new ABCDMoznost{ Text="Zoznam objektov bez indexov", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });

            // Otázka 2 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Ako správne deklarujeme prázdne dvojrozmerné pole 4x3 typu int?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="int[,] pole = new int[3,4];", Index=0 },
                    new ABCDMoznost{ Text="int[,] pole = new int[4,3];", Index=1 },
                    new ABCDMoznost{ Text="int[] pole = new int[4,3];", Index=2 },
                    new ABCDMoznost{ Text="int[4,3] pole;", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 3 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Čo sa stane, ak použijeme neexistujúci index?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Program sa automaticky opraví", Index=0 },
                    new ABCDMoznost{ Text="Vráti sa hodnota 0", Index=1 },
                    new ABCDMoznost{ Text="Vznikne chyba (IndexOutOfRangeException)", Index=2 },
                    new ABCDMoznost{ Text="Pole sa zväčší", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });

            // Otázka 4 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Doplň metódu na zistenie počtu stĺpcov:\n\npole.________(1);",
                SpravnaOdpoved = "GetLength"
            });

            // Otázka 5 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Doplň inicializáciu náhodného čísla do poľa:\n\npole[i,j] = random.________(100);",
                SpravnaOdpoved = "Next"
            });

            // Otázka 6 - Párovacia (Funkcie a ich význam)
            var parovaciaOtazka1 = new ParovaciaOtazka
            {
                OtazkaText = "Spoj funkcie s ich významom"
            };

            // Ľavý stĺpec
            parovaciaOtazka1.LavyStlpec.Add(new ParovaciaPolozka { Text = "pole.Length", Index = 0, IsLeft = true });
            parovaciaOtazka1.LavyStlpec.Add(new ParovaciaPolozka { Text = "pole.GetLength(0)", Index = 1, IsLeft = true });
            parovaciaOtazka1.LavyStlpec.Add(new ParovaciaPolozka { Text = "pole.GetLength(1)", Index = 2, IsLeft = true });
            parovaciaOtazka1.LavyStlpec.Add(new ParovaciaPolozka { Text = "pole[i,j]", Index = 3, IsLeft = true });

            // Pravý stĺpec
            parovaciaOtazka1.PravyStlpec.Add(new ParovaciaPolozka { Text = "Počet stĺpcov", Index = 0, IsLeft = false });
            parovaciaOtazka1.PravyStlpec.Add(new ParovaciaPolozka { Text = "Prístup ku konkrétnemu prvku", Index = 1, IsLeft = false });
            parovaciaOtazka1.PravyStlpec.Add(new ParovaciaPolozka { Text = "Celkový počet prvkov", Index = 2, IsLeft = false });
            parovaciaOtazka1.PravyStlpec.Add(new ParovaciaPolozka { Text = "Počet riadkov", Index = 3, IsLeft = false });

            // Správne páry
            parovaciaOtazka1.SpravnePary.Add(new ParovaciaPolicka { Lava = "pole.Length", Prava = "Celkový počet prvkov" });
            parovaciaOtazka1.SpravnePary.Add(new ParovaciaPolicka { Lava = "pole.GetLength(0)", Prava = "Počet riadkov" });
            parovaciaOtazka1.SpravnePary.Add(new ParovaciaPolicka { Lava = "pole.GetLength(1)", Prava = "Počet stĺpcov" });
            parovaciaOtazka1.SpravnePary.Add(new ParovaciaPolicka { Lava = "pole[i,j]", Prava = "Prístup ku konkrétnemu prvku" });

            Otazky.Add(parovaciaOtazka1);

            // Otázka 7 - Párovacia (Pojem - Význam)
            var parovaciaOtazka2 = new ParovaciaOtazka
            {
                OtazkaText = "Spoj pojmy s ich významom"
            };

            // Ľavý stĺpec
            parovaciaOtazka2.LavyStlpec.Add(new ParovaciaPolozka { Text = "2D pole", Index = 0, IsLeft = true });
            parovaciaOtazka2.LavyStlpec.Add(new ParovaciaPolozka { Text = "Index", Index = 1, IsLeft = true });
            parovaciaOtazka2.LavyStlpec.Add(new ParovaciaPolozka { Text = "Random", Index = 2, IsLeft = true });
            parovaciaOtazka2.LavyStlpec.Add(new ParovaciaPolozka { Text = "foreach", Index = 3, IsLeft = true });

            // Pravý stĺpec (premiešané poradie - nie vedľa seba)
            parovaciaOtazka2.PravyStlpec.Add(new ParovaciaPolozka { Text = "Prechod všetkých prvkov", Index = 0, IsLeft = false });
            parovaciaOtazka2.PravyStlpec.Add(new ParovaciaPolozka { Text = "Generovanie náhodných čísel", Index = 1, IsLeft = false });
            parovaciaOtazka2.PravyStlpec.Add(new ParovaciaPolozka { Text = "Dáta usporiadané do tabuľky", Index = 2, IsLeft = false });
            parovaciaOtazka2.PravyStlpec.Add(new ParovaciaPolozka { Text = "Poradie prvku", Index = 3, IsLeft = false });

            // Správne páry
            parovaciaOtazka2.SpravnePary.Add(new ParovaciaPolicka { Lava = "2D pole", Prava = "Dáta usporiadané do tabuľky" });
            parovaciaOtazka2.SpravnePary.Add(new ParovaciaPolicka { Lava = "Index", Prava = "Poradie prvku" });
            parovaciaOtazka2.SpravnePary.Add(new ParovaciaPolicka { Lava = "Random", Prava = "Generovanie náhodných čísel" });
            parovaciaOtazka2.SpravnePary.Add(new ParovaciaPolicka { Lava = "foreach", Prava = "Prechod všetkých prvkov" });

            Otazky.Add(parovaciaOtazka2);
        }
    }
}
