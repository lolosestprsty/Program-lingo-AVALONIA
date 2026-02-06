using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.LevelManager.Otazky;

public abstract class OtazkaBase
{
    public string OtazkaText { get; set; } = "";
    public bool JeZodpovedana { get; set; }

    public abstract bool SkontrolujOdpoved(object odpoved);
}
