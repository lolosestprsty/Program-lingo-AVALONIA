using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.ViewModels
{
    public partial class LevelyOverviewModel : ViewModelBase
    {
        private readonly MainViewModel _main;

        public LevelyOverviewModel(MainViewModel main)
        {
            _main = main;
            LoadLevels();
        }

        #region Properties
        public ObservableCollection<LevelyItemModel> LevelCollection { get; set; }
        #endregion
        #region commands
        public IRelayCommand<LevelyItemModel> LevelSelectedCommand =>
            new RelayCommand<LevelyItemModel>(level =>
            {
                Debug.WriteLine($"Level selected: {level?.LevelName}");
                if (level?.LevelName == "1")
                {
                    _main.ShowLevel1Command.Execute(null);
                }
            });

        #endregion
        #region Methods
        private void LoadLevels()
        {
            LevelCollection = new ObservableCollection<LevelyItemModel>();

            for (int i = 1; i <= 10; i++)
            {
                LevelCollection.Add(new LevelyItemModel
                {
                    LevelName = i.ToString()
                });
            }
        }
        #endregion

    }
}
