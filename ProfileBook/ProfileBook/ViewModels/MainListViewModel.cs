using Prism;
using Prism.Commands;
using Prism.Ioc;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.DbService;
using ProfileBook.Services.Settings;
using ProfileBook.View;
using ProfileBook.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class MainListViewModel : ViewModelBase, IInitializeAsync
    {
        public MainListViewModel(INavigationService navigationService, IDbService dbService, ISettingsManagerService settingsManager) : base(navigationService, dbService, settingsManager)
        {
            Title = "Main List";
        }


        #region Initialize

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            List<ProfileModel> profiles = await DbService.GetOwnersProfilesAsync(SettingsManager.LoggedUser);
            ProfileList = new ObservableCollection<ProfileModel>(profiles);
        }

        #endregion


        #region Private fields

        private ObservableCollection<ProfileModel> profileList;

        #endregion


        #region Properties

        public ObservableCollection<ProfileModel> ProfileList
        {
            get => profileList;
            set => SetProperty(ref profileList, value);
        }

        #endregion


        #region Commands

        public DelegateCommand LogOutTapCommand => new DelegateCommand(GoLogOut);
        public DelegateCommand AddEditProfileTapCommand => new DelegateCommand(GoAddEditProfile);

        #endregion


        #region Private helpers

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