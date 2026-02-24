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
                    _main.Level3Completed = true;
                    _main.Level3Failed = false;
                }
                else
                {
                    _main.Level3Failed = true;
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

            // Otázka 13-16 - Spoj dvojice (dátové typy s význammi)
            var parovaciaOtazka = new ParovaciaOtazka
            {
                OtazkaText = "Spoj dvojice - Dátové typy a ich významy"
            };

            // Ľavý stĺpec - dátové typy
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "int", Index = 0, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "double", Index = 1, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "char", Index = 2, IsLeft = true });
            parovaciaOtazka.LavyStlpec.Add(new ParovaciaPolozka { Text = "string", Index = 3, IsLeft = true });

            // Pravý stĺpec - významy (v inom poradí)
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "desatinné číslo", Index = 0, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "jeden znak", Index = 1, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "text", Index = 2, IsLeft = false });
            parovaciaOtazka.PravyStlpec.Add(new ParovaciaPolozka { Text = "celé číslo", Index = 3, IsLeft = false });

            // Správne páry
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "int", Prava = "celé číslo" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "double", Prava = "desatinné číslo" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "char", Prava = "jeden znak" });
            parovaciaOtazka.SpravnePary.Add(new ParovaciaPolicka { Lava = "string", Prava = "text" });

            Otazky.Add(parovaciaOtazka);
        }
    }
}
