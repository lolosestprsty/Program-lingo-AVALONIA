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
        public Level1Model(MainViewModel main)
        {
            _main = main;
            NacitajOtazky();
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

        private int _index;


        private void NacitajOtazky()
        {
            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Ktorá spoločnosť vyvinula jazyk C#?",
                Moznosti = ["Apple", "Google", "Microsoft", "IBM"],
                SpravnaMoznostIndex = 2
            });

            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "V ktorom roku bol jazyk C# predstavený spolu s .NET Framework?",
                Moznosti = ["1998", "2002", "2005", "2010"],
                SpravnaMoznostIndex = 1
            });

            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Akú príponu majú zdrojové súbory jazyka C#?",
                Moznosti = [".cpp", ".java", ".cs", ".csharp"],
                SpravnaMoznostIndex = 2
            });

            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Je jazyk C# case sensitive?",
                Moznosti = ["Áno", "Nie"],
                SpravnaMoznostIndex = 0
            });

            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Ktorá z možností NIE JE uvedená ako využitie C#?",
                Moznosti = ["Desktopové aplikácie", "Mobilné aplikácie", "Programovanie mikrovlniek", "Počítačové hry"],
                SpravnaMoznostIndex = 2
            });

            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Ako sa nazýva automatická správa pamäte v C#?",
                Moznosti = ["Memory Cleaner", "Garbage Collector", "Memory Manager", "AutoDelete"],
                SpravnaMoznostIndex = 1
            });

            Otazky.Add(new ABCDOtazka
            {
                OtazkaText = "Ktoré vývojové prostredie sa odporúča na začiatok s C#?",
                Moznosti = ["PyCharm", "Eclipse", "Microsoft Visual Studio", "NetBeans"],
                SpravnaMoznostIndex = 2
            });
        }

        public IRelayCommand<object> OdpovedCommand =>
            new RelayCommand<object>(odpoved =>
            {
                bool spravna = AktualnaOtazka!.SkontrolujOdpoved(odpoved);

                if (spravna)
                    Progres = (int)((double)(_index + 1) / Otazky.Count * 100);
                else
                {
                    Otazky.Remove(AktualnaOtazka);
                    Otazky.Add(AktualnaOtazka);
                }

                DalsiaOtazka();
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
