using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.LevelManager.Otazky
{
    public abstract partial class OtazkaBase : ObservableObject
    {
        public string OtazkaText { get; set; } = "";
        public IRelayCommand<object>? OdpovedCommand { get; set; }
        public abstract bool SkontrolujOdpoved(object? odpoved);
    }
}
