using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaApplication1.VysvetlivkyManager;
using AvaloniaApplication1.VysvetlivkyManager.VysvetlivkyModels;
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
        [RelayCommand]
        private void ShowVysvetlivka3()
        {
            SelectedVysvetlivka = new Vysvetlivka3Model();
        }
        [RelayCommand]
        private void ShowVysvetlivka4()
        {
            SelectedVysvetlivka = new Vysvetlivka4Model();
        }
        [RelayCommand]
        private void ShowVysvetlivka5()
        {
            SelectedVysvetlivka = new Vysvetlivka5Model();
        }
        [RelayCommand]
        private void ShowVysvetlivka6()
        {
            SelectedVysvetlivka = new Vysvetlivka6Model();
        }
        [RelayCommand]
        private void ShowVysvetlivka7()
        {
            SelectedVysvetlivka = new Vysvetlivka7Model();
        }
        [RelayCommand]
        private void ShowVysvetlivka8()
        {
            SelectedVysvetlivka = new Vysvetlivka8Model();
        }
        [RelayCommand]
        private void ShowVysvetlivka9()
        {
            SelectedVysvetlivka = new Vysvetlivka9Model();
        }
        [RelayCommand]
        private void ShowVysvetlivka10()
        {
            SelectedVysvetlivka = new Vysvetlivka10Model();
        }
        [RelayCommand]
        private void ShowVysvetlivka11()
        {
            SelectedVysvetlivka = new Vysvetlivka11Model();
        }
    }
}
