using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Services.DbService;
using ProfileBook.Services.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SettingsViewModel : ViewModelBase, INavigatedAware
    {
        public SettingsViewModel(INavigationService navigationService, IDbService dbService, ISettingsManagerService settingsManager) : base(navigationService, dbService, settingsManager)
        {
            Title = "Settings";
        }


        #region Private fields

        private object sortSelection;
        private string oldSortSelection;

        #endregion


        #region Properties

        public object SortSelection
        {
            get => sortSelection;
            set => SetProperty(ref sortSelection, value);
        }

        #endregion


        #region Overrides

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            string sortList = SettingsManager.SortListBy;
            if (!string.IsNullOrEmpty(sortList))
            {
                switch (sortList)
                {
                    case "Name":
                        SortSelection = "Name";
                        break;
                    case "NickName":
                        SortSelection = "NickName";
                        break;
                    case "Date creation":
                        SortSelection = "Date creation";
                        break;
                }
                oldSortSelection = sortList;
            }
        }


        #endregion



        #region Public methods

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            if(oldSortSelection != SortSelection.ToString())
            {
                SettingsManager.SortListBy = SortSelection.ToString();
                SettingsManager.ChangeSort = true;
            }
        }

        public void OnNavigatedTo(INavigationParameters parameters) { }

        #endregion
    }
}