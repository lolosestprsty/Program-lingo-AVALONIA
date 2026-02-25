using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.ViewModels
{
    public partial class NastaveniaOverviewModel: ViewModelBase
    {
        private readonly MainViewModel _main;
        private const string AdminPassword = "123456";

        public NastaveniaOverviewModel(MainViewModel main) 
        {
            _main = main;
        }

        [ObservableProperty]
        private string adminPasswordInput = string.Empty;

        [ObservableProperty]
        private string loginMessage = string.Empty;

        [ObservableProperty]
        private bool isAdmin;

        [ObservableProperty]
        private bool loginSuccess;

        [ObservableProperty]
        private Color loginMessageColor = Colors.Black;

        [RelayCommand]
        private void AdminLogin()
        {
            if (AdminPasswordInput == AdminPassword)
            {
                IsAdmin = true;
                LoginSuccess = true;
                _main.Level1Completed = true;
                _main.Level2Completed = true;
                _main.Level3Completed = true;
                _main.Level4Completed = true;
                _main.Level5Completed = true;
                _main.Level6Completed = true;
                _main.Level7Completed = true;
                _main.Level8Completed = true;
                LoginMessage = string.Empty;
                AdminPasswordInput = string.Empty;
            }
            else
            {
                IsAdmin = false;
                LoginSuccess = false;
                LoginMessage = "Nesprávne heslo!";
                LoginMessageColor = Colors.Red;
            }
        }

        [RelayCommand]
        private void AdminLogout()
        {
            IsAdmin = false;
            LoginSuccess = false;
            _main.Level1Completed = false;
            _main.Level2Completed = false;
            _main.Level3Completed = false;
            _main.Level4Completed = false;
            _main.Level5Completed = false;
            _main.Level6Completed = false;
            _main.Level7Completed = false;
            _main.Level8Completed = false;
            LoginMessage = string.Empty;
            AdminPasswordInput = string.Empty;
        }
    }
}
