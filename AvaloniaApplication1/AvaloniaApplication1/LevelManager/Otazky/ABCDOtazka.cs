using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.LevelManager.Otazky;

public class ABCDOtazka : OtazkaBase
{
    public List<string> Moznosti { get; set; } = [];
    public int SpravnaMoznostIndex { get; set; }

    public override bool SkontrolujOdpoved(object odpoved)
        => odpoved is int index && index == SpravnaMoznostIndex;
}
