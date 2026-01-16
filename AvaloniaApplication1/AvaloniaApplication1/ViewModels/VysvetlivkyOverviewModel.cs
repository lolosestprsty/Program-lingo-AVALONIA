using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaApplication1.VysvetlivkyManager;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.ViewModels
{
    public partial class VysvetlivkyOverviewModel : ViewModelBase
    {
        public VysvetlivkyOverviewModel()
        {
            SelectedVysvetlivka = new Vysvetlivka1Model();
        }

        [ObservableProperty]
        private object? selectedVysvetlivka;
        [RelayCommand]
        private void ShowVysvetlivka1()
        {
            SelectedVysvetlivka = new Vysvetlivka1Model();
        }
        [RelayCommand]
        private void ShowVysvetlivka2()
        {
            SelectedVysvetlivka = new Vysvetlivka2Model();

        }
    }
}
