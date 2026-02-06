using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.LevelManager.Otazky;
public class VstupnaOtazka : OtazkaBase
{
    public string SpravnaOdpoved { get; set; } = "";

    public override bool SkontrolujOdpoved(object odpoved)
        => odpoved is string text &&
           text.Trim().Equals(SpravnaOdpoved, StringComparison.OrdinalIgnoreCase);
}
