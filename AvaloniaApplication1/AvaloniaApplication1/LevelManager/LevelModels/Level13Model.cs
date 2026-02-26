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
    public partial class Level13Model : ViewModelBase
    {
        private readonly MainViewModel _main;
        private readonly int _initialCount;
        private int _index = 0;
        private int _correctCount = 0;
        private int _answeredCount = 0;

        public Level13Model(MainViewModel main)
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
                    _main.Level13Completed = true;
                    _main.Level13Failed = false;
                }
                else
                {
                    _main.Level13Failed = true;
                }
                _main.ShowLevelyOverviewCommand.Execute(null);
            });

        private void NacitajOtazky()
        {
            // Načítaj dáta z JSON pomocou helper metódy
            var otazkyZJson = Data.QuestionConverter.ConvertToOtazky(13);

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
                OtazkaText = "Na čo slúži using System.IO; ?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Na prácu s grafikou", Index=0 },
                    new ABCDMoznost{ Text="Na vstupno-výstupné operácie so súbormi", Index=1 },
                    new ABCDMoznost{ Text="Na prácu s databázou", Index=2 },
                    new ABCDMoznost{ Text="Na matematické výpočty", Index=3 },
                },
                SpravnaMoznostIndex = 1
            });

            // Otázka 2 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Čo sa stane pri použití FileMode.Open, ak súbor neexistuje?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="Súbor sa vytvorí", Index=0 },
                    new ABCDMoznost{ Text="Súbor sa prepíše", Index=1 },
                    new ABCDMoznost{ Text="Nastane chyba", Index=2 },
                    new ABCDMoznost{ Text="Súbor sa otvorí v režime append", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });

            // Otázka 3 - ABCD
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Ktorá trieda dokáže zapisovať textové reťazce do súboru?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="FileStream", Index=0 },
                    new ABCDMoznost{ Text="StreamReader", Index=1 },
                    new ABCDMoznost{ Text="StreamWriter", Index=2 },
                    new ABCDMoznost{ Text="Console", Index=3 },
                },
                SpravnaMoznostIndex = 2
            });

            // Otázka 4 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Doplň správny FileMode (ak súbor existuje, doplní obsah na koniec):\n\nFileStream file = new FileStream(\"data.txt\", FileMode.________);",
                SpravnaOdpoved = "Append"
            });

            // Otázka 5 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Doplň názov triedy na čítanie textu zo súboru:\n\nFileStream file = new FileStream(\"data.txt\", FileMode.Open);\n________ sr = new ________(file);",
                SpravnaOdpoved = "StreamReader"
            });

            // Otázka 6 - Vstupná
            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Doplň podmienku ukončenia čítania (keď je koniec súboru):\n\nstring riadok = \"\";\nwhile (riadok != ______)\n{\n    riadok = sr.ReadLine();\n}",
                SpravnaOdpoved = "null"
            });

            // Otázka 7 - Párovacia (FileMode s významom)
            var parovaciaOtazka1 = new ParovaciaOtazka
            {
                OtazkaText = "Spoj FileMode s jeho významom"
            };

            // Ľavý stĺpec
            parovaciaOtazka1.LavyStlpec.Add(new ParovaciaPolozka { Text = "Create", Index = 0, IsLeft = true });
            parovaciaOtazka1.LavyStlpec.Add(new ParovaciaPolozka { Text = "CreateNew", Index = 1, IsLeft = true });
            parovaciaOtazka1.LavyStlpec.Add(new ParovaciaPolozka { Text = "Open", Index = 2, IsLeft = true });
            parovaciaOtazka1.LavyStlpec.Add(new ParovaciaPolozka { Text = "Append", Index = 3, IsLeft = true });

            // Pravý stĺpec (premiešané poradie - nie vedľa seba)
            parovaciaOtazka1.PravyStlpec.Add(new ParovaciaPolozka { Text = "Otvorí existujúci súbor, inak chyba", Index = 0, IsLeft = false });
            parovaciaOtazka1.PravyStlpec.Add(new ParovaciaPolozka { Text = "Vytvorí nový súbor, ak existuje → chyba", Index = 1, IsLeft = false });
            parovaciaOtazka1.PravyStlpec.Add(new ParovaciaPolozka { Text = "Vytvorí nový alebo prepíše existujúci", Index = 2, IsLeft = false });
            parovaciaOtazka1.PravyStlpec.Add(new ParovaciaPolozka { Text = "Doplní obsah na koniec súboru", Index = 3, IsLeft = false });

            // Správne páry
            parovaciaOtazka1.SpravnePary.Add(new ParovaciaPolicka { Lava = "Create", Prava = "Vytvorí nový alebo prepíše existujúci" });
            parovaciaOtazka1.SpravnePary.Add(new ParovaciaPolicka { Lava = "CreateNew", Prava = "Vytvorí nový súbor, ak existuje → chyba" });
            parovaciaOtazka1.SpravnePary.Add(new ParovaciaPolicka { Lava = "Open", Prava = "Otvorí existujúci súbor, inak chyba" });
            parovaciaOtazka1.SpravnePary.Add(new ParovaciaPolicka { Lava = "Append", Prava = "Doplní obsah na koniec súboru" });

            Otazky.Add(parovaciaOtazka1);

            // Otázka 8 - Párovacia (Trieda s funkciou)
            var parovaciaOtazka2 = new ParovaciaOtazka
            {
                OtazkaText = "Spoj triedu s jej funkciou"
            };

            // Ľavý stĺpec
            parovaciaOtazka2.LavyStlpec.Add(new ParovaciaPolozka { Text = "FileStream", Index = 0, IsLeft = true });
            parovaciaOtazka2.LavyStlpec.Add(new ParovaciaPolozka { Text = "StreamReader", Index = 1, IsLeft = true });
            parovaciaOtazka2.LavyStlpec.Add(new ParovaciaPolozka { Text = "StreamWriter", Index = 2, IsLeft = true });
            parovaciaOtazka2.LavyStlpec.Add(new ParovaciaPolozka { Text = "ReadLine()", Index = 3, IsLeft = true });

            // Pravý stĺpec (premiešané poradie - nie vedľa seba)
            parovaciaOtazka2.PravyStlpec.Add(new ParovaciaPolozka { Text = "Číta text zo súboru", Index = 0, IsLeft = false });
            parovaciaOtazka2.PravyStlpec.Add(new ParovaciaPolozka { Text = "Načíta jeden riadok textu", Index = 1, IsLeft = false });
            parovaciaOtazka2.PravyStlpec.Add(new ParovaciaPolozka { Text = "Nastavuje prístup k súboru (pracuje s bajtmi)", Index = 2, IsLeft = false });
            parovaciaOtazka2.PravyStlpec.Add(new ParovaciaPolozka { Text = "Zapisuje text do súboru", Index = 3, IsLeft = false });

            // Správne páry
            parovaciaOtazka2.SpravnePary.Add(new ParovaciaPolicka { Lava = "FileStream", Prava = "Nastavuje prístup k súboru (pracuje s bajtmi)" });
            parovaciaOtazka2.SpravnePary.Add(new ParovaciaPolicka { Lava = "StreamReader", Prava = "Číta text zo súboru" });
            parovaciaOtazka2.SpravnePary.Add(new ParovaciaPolicka { Lava = "StreamWriter", Prava = "Zapisuje text do súboru" });
            parovaciaOtazka2.SpravnePary.Add(new ParovaciaPolicka { Lava = "ReadLine()", Prava = "Načíta jeden riadok textu" });

            Otazky.Add(parovaciaOtazka2);
        }
    }
}
