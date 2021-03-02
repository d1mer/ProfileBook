using Prism;
using Prism.Commands;
using Prism.Ioc;
using Prism.Navigation;
using ProfileBook.Services.DbService;
using ProfileBook.Services.Settings;
using ProfileBook.View;
using ProfileBook.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class MainListViewModel : ViewModelBase
    {
        public MainListViewModel(INavigationService navigationService, IDbService dbService, ISettingsManagerService settingsManager) : base(navigationService, dbService, settingsManager)
        {
            Title = "Main List";
        }


        #region Commands

        public DelegateCommand LogOutTapCommand => new DelegateCommand(GoLogOut);
        public DelegateCommand AddEditProfileTapCommand => new DelegateCommand(GoAddEditProfile);

        #endregion



        #region Helpers

        private async void GoLogOut()
        {
            SettingsManager.LoggedUser = "";
            await NavigationService.NavigateAsync(nameof(UserSignIn));

            NavigationPage page = (NavigationPage)App.Current.MainPage;
            while (page.Navigation.NavigationStack.Count > 1)
                page.Navigation.RemovePage(page.Navigation.NavigationStack[page.Navigation.NavigationStack.Count - 2]);
        }


        private async void GoAddEditProfile()
        {
            await NavigationService.NavigateAsync(nameof(AddEditProfileView));
        }

        #endregion
    }
}