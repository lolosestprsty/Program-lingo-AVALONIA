using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.LevelManager.Otazky;

public partial class ABCDOtazka : OtazkaBase
{
    public List<ABCDMoznost> Moznosti { get; set; } = new List<ABCDMoznost>();
    public int SpravnaMoznostIndex { get; set; }
    public IRelayCommand<object>? OdpovedCommand { get; set; }

    public override bool SkontrolujOdpoved(object odpoved)
        => odpoved is int index && index == SpravnaMoznostIndex;
}
