using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Prism.Commands;
using Prism.Mvvm;
using ProfileBook.View;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.Settings;
using Acr.UserDialogs;
using ProfileBook.ServiceData.Constants;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class UserSignInViewModel : ViewModelBase
    {
        #region fields

        string userLogin;
        string userPassword;
        bool   isEnabled;

        #endregion


        #region properties

        public string UserLogin
        {
            get => userLogin;
            set => SetProperty(ref userLogin, value);
        }

        public string UserPassword
        {
            get => userPassword;
            set => SetProperty(ref userPassword, value);
        }

        public bool IsEnabled
        {
            get => isEnabled;
            set => SetProperty(ref isEnabled, value);
        }

        #endregion


        #region commands

        public DelegateCommand OnSignUpTapCommand { get; private set; }
        public DelegateCommand OnSignInTapCommand { get; private set; }

        #endregion

        public UserSignInViewModel(INavigationService navigationService, ISettingsManagerService settingsManager) : base(navigationService)
        {
            OnSignUpTapCommand = new DelegateCommand(GoSignUp);
            OnSignInTapCommand = new DelegateCommand(AuthorizationUser, CanExecute);

            SettingsManager = settingsManager;

            Title = "Users SignIn";
        }



        #region private helpers

        private async void GoSignUp() => await NavigationService.NavigateAsync(nameof(UserSignUp));

        public override void Initialize(INavigationParameters parameters)
        {
            UserLogin = (string)parameters["login"];
        }


        private async void AuthorizationUser()
        {
            Authorization authorizationUser = new Authorization();
            bool result = await authorizationUser.IsAuthorization(UserLogin, UserPassword);

            if (result)
            {
                SettingsManager.LoggedUser = UserLogin;

                await NavigationService.NavigateAsync(nameof(MainListView));

                // clear navigation stack
                NavigationPage page = (NavigationPage)App.Current.MainPage;
                while(page.Navigation.NavigationStack.Count > 1)
                    page.Navigation.RemovePage(page.Navigation.NavigationStack[page.Navigation.NavigationStack.Count - 2]);

                UserLogin = "";
                UserPassword = "";
            }
            else
            {
                UserDialogs.Instance.Alert(Constants.INVALID_LOGIN_OR_PASSWORD);
                UserPassword = "";
            }
        }


        private bool CanExecute() => IsEnabled;

        #endregion
    }
}