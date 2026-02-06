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
                OtazkaText = "Čo je if?",
                Moznosti = ["cyklus", "podmienka", "premenná", "trieda"],
                SpravnaMoznostIndex = 1
            });

            Otazky.Add(new VstupnaOtazka
            {
                OtazkaText = "Napíš kľúčové slovo pre podmienku",
                SpravnaOdpoved = "if"
            });
        }

        public IRelayCommand<object> OdpovedCommand =>
            new RelayCommand<object>(odpoved =>
            {
                bool spravna = AktualnaOtazka!.SkontrolujOdpoved(odpoved);

                if (spravna)
                    Progres += 100 / Otazky.Count;
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
