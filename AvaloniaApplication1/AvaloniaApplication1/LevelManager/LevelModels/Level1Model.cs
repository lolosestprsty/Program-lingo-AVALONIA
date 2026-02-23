using Avalonia.Media;
using AvaloniaApplication1.LevelManager.Otazky;
using AvaloniaApplication1.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;

namespace AvaloniaApplication1.LevelManager.LevelModels
{
    public partial class Level1Model : ViewModelBase
    {
        private readonly MainViewModel _main;
        private readonly int _initialCount;
        private int _index = 0;
        private int _correctCount = 0;
        public Level1Model(MainViewModel main)
        {
            _main = main;

            //nacitanie
            NacitajOtazky();

            // remember initial count to compute progress
            _initialCount = Otazky.Count;

            // initialize index and counts
            _index = 0;
            _correctCount = 0;

            //definovanie commandu
            OdpovedCommand = new RelayCommand<object>(odpoved =>
            {
                if (AktualnaOtazka is null)
                    return;

                bool spravna = AktualnaOtazka.SkontrolujOdpoved(odpoved);

                if (spravna)
                {
                    _correctCount++;
                }

                // update progress (based on how many answered correctly so far)
                Progres = (int)((double)_correctCount / _initialCount * 100);

                // move to next question
                _index++;

                if (_index >= Otazky.Count)
                {
                    // finished - show summary
                    CorrectCount = _correctCount;
                    IsFinished = true;
                    return;
                }

                AktualnaOtazka = Otazky[_index];
            });

            //command ku kazdej otazke (set the command on question instances so views that use it directly can call it)
            foreach (var otazka in Otazky)
                if (otazka is ABCDOtazka a)
                    a.OdpovedCommand = OdpovedCommand;

            //prva otazka je aktualna
            AktualnaOtazka = Otazky.First();
        }

        public IRelayCommand BackCommand =>
            new RelayCommand(() =>
            {
                _main.ShowLevelyOverviewCommand.Execute(null);
            });


        //otazky
        public ObservableCollection<OtazkaBase> Otazky { get; } = new();

        [ObservableProperty]
        private OtazkaBase? aktualnaOtazka;

        [ObservableProperty]
        private int progres; // 0–100

        [ObservableProperty]
        private int correctCount;

        [ObservableProperty]
        private bool isFinished;

        // index already declared above


        private void NacitajOtazky()
        {
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Ktorá spoločnosť vyvinula jazyk C#?",
                Moznosti = new()
                {
                new ABCDMoznost{ Text="Apple", Index=0 },
                new ABCDMoznost{ Text="Google", Index=1 },
                new ABCDMoznost{ Text="Microsoft", Index=2 },
                new ABCDMoznost{ Text="IBM", Index=3 }, },
                SpravnaMoznostIndex = 2
            });

            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "V ktorom roku bol jazyk C# predstavený spolu s .NET Framework?",
                Moznosti = new() { 
                    new ABCDMoznost { Text = "1998", Index = 0 }, 
                    new ABCDMoznost { Text = "2002", Index = 1 }, 
                    new ABCDMoznost { Text = "2005", Index = 2 }, 
                    new ABCDMoznost { Text = "2010", Index = 3 }, },
                SpravnaMoznostIndex = 1
            });

            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Akú príponu majú zdrojové súbory jazyka C#?",
                Moznosti = new() { 
                    new ABCDMoznost { Text = ".cpp", Index = 0 }, 
                    new ABCDMoznost { Text = ".java", Index = 1 }, 
                    new ABCDMoznost { Text = ".cs", Index = 2 }, 
                    new ABCDMoznost { Text = ".csharp", Index = 3 }, },
                SpravnaMoznostIndex = 2
            });

            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Je jazyk C# case sensitive?",
                Moznosti = new()
                {
                    new ABCDMoznost{ Text="ÁNO", Index = 0},
                    new ABCDMoznost{ Text="NIE", Index = 1},
                },
                SpravnaMoznostIndex = 0
            });

            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Ktorá z možností NIE JE uvedená ako využitie C#?",
                Moznosti = new() { 
                    new ABCDMoznost { Text = "Desktopové aplikácie", Index = 0 }, 
                    new ABCDMoznost { Text = "Mobilné aplikácie", Index = 1 }, 
                    new ABCDMoznost { Text = "Programovanie mikrovlniek", Index = 2 }, 
                    new ABCDMoznost { Text = "Počítačové hry", Index = 3 }, },
                SpravnaMoznostIndex = 2
            });

            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Ako sa nazýva automatická správa pamäte v C#?",
                Moznosti = new() { 
                    new ABCDMoznost { Text = "Memory Cleaner", Index = 0 }, 
                    new ABCDMoznost { Text = "Garbage Collector", Index = 1 }, 
                    new ABCDMoznost { Text = "Memory Manager", Index = 2 }, 
                    new ABCDMoznost { Text = "AutoDelete", Index = 3 }, },
                SpravnaMoznostIndex = 1
            });

            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Ktoré vývojové prostredie sa odporúča na začiatok s C#?",
                Moznosti = new() { 
                    new ABCDMoznost { Text = "PyCharm", Index = 0 }, 
                    new ABCDMoznost { Text = "Eclipse", Index = 1 }, 
                    new ABCDMoznost { Text = "Microsoft Visual Studio", Index = 2 }, 
                    new ABCDMoznost { Text = "NetBeans", Index = 3 }, },
                SpravnaMoznostIndex = 2
            });
        }

        public IRelayCommand<object> OdpovedCommand { get; set; }

        public IRelayCommand OkCommand =>
            new RelayCommand(() =>
            {
                // only unlock next level when all questions were answered correctly
                if (CorrectCount == _initialCount)
                {
                    _main.Level1Completed = true;
                }
                // return to overview regardless
                _main.ShowLevelyOverviewCommand.Execute(null);
            });

        private void DalsiaOtazka()
        {
            _index++;

            if (_index >= Otazky.Count)
            {
                _main.ShowLevelyOverviewCommand.Execute(null);
                return;
            }

            AktualnaOtazka = Otazky[_index];
        }
    }
}
