using AvaloniaApplication1.LevelManager.Otazky;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AvaloniaApplication1.LevelManager.OtazkyModels;

public partial class ABCDOtazkaViewModel : ObservableObject
{
    public string OtazkaText { get; set; }

    public ObservableCollection<ABCDMoznost> Moznosti { get; set; }

    public ICommand OdpovedCommand { get; }

    public ABCDOtazkaViewModel()
    {
        OtazkaText = "Aký je výsledok 2 + 2?";

        Moznosti = new ObservableCollection<ABCDMoznost>
        {
            new ABCDMoznost { Text = "3", Index = 0 },
            new ABCDMoznost { Text = "4", Index = 1 },
            new ABCDMoznost { Text = "5", Index = 2 },
            new ABCDMoznost { Text = "6", Index = 3 },
        };

        OdpovedCommand = new RelayCommand<int>(VyhodnotOdpoved);
    }

    private void VyhodnotOdpoved(int index)
    {
        // zatiaľ len test
        Console.WriteLine($"Klikol si na {index}");
    }
}