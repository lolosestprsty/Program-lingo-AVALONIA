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
        public LevelyOverviewModel() 
        {
            LoadLevels();
        }

        #region Properties
        ObservableCollection<LevelyItemModel> _levelCollection;
        public ObservableCollection<LevelyItemModel> LevelCollection
        {
            get => _levelCollection;
            set => SetProperty(ref _levelCollection, value);
        }
        #endregion
        #region commands
        IRelayCommand<LevelyItemModel> _levelSelectedCommand;
        public IRelayCommand<LevelyItemModel> LevelSelectedCommand
            => _levelSelectedCommand ??= new RelayCommand<LevelyItemModel>(level =>
            {
                // Handle level selection logic here
                Debug.WriteLine($"Level selected: {level.LevelName}");
            });

        #endregion
        #region Methods
        public void LoadLevels()
        {
            var collection = new ObservableCollection<LevelyItemModel>();

            int startIndex = collection.Count + 1;
            for (int i = 0; i < 10; i++)
            {
                int index = startIndex + i;
                collection.Add(new LevelyItemModel
                {
                    LevelName = index.ToString(),
                    LevelDescription = $"Additional level {index}"
                });
            }

            LevelCollection = collection;
        }
        #endregion

    }
}
