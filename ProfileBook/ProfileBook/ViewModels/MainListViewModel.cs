using Acr.UserDialogs;
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
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

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
        public ICommand DeleteTapCommand => new Command(OnDeleteAsync);

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


        private async void OnDeleteAsync(object selectedProfile)
        {
            ProfileModel profileModel = selectedProfile as ProfileModel;

            if (profileModel != null)
            {
                ConfirmConfig confirmConfig = new ConfirmConfig
                {
                    Message = "Are you really want to delete this profile?",
                    OkText = "Delete",
                    CancelText = "Cancel"
                };

                if (await UserDialogs.Instance.ConfirmAsync(confirmConfig))
                {
                    ProfileList.Remove(ProfileList.First(p => p.Id == profileModel.Id));
                    await DbService.DeleteDataAsync(profileModel);
                }
            }
        }

        #endregion
    }
}