using Prism;
using Prism.Commands;
using Prism.Ioc;
using Prism.Navigation;
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
        #region Fields

          

        #endregion

        public MainListViewModel(INavigationService navigationService, ISettingsManagerService settingsManager) : base(navigationService)
        {
            LogOutTapCommand = new DelegateCommand(GoLogOut);
            AddEditProfileTapCommand = new DelegateCommand(GoAddEditProfile);
            SettingsManager = settingsManager;
            Title = "Main List";
        }


        #region Commands

        public DelegateCommand LogOutTapCommand { get; }
        public DelegateCommand AddEditProfileTapCommand { get; }

        #endregion


        #region Properties


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