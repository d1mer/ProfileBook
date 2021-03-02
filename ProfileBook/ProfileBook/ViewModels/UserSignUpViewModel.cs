using System;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Commands;
using ProfileBook.ServiceData.Enums;
using ProfileBook.ServiceData.Constants;
using ProfileBook.Services.Authentication;
using Acr.UserDialogs;
using ProfileBook.Services.Repository;
using ProfileBook.View;

namespace ProfileBook.ViewModels
{
    public class UserSignUpViewModel : ViewModelBase
    {
        #region fields

        string userLogin = "";
        string userPassword = "";
        string confirmUserPassword = "";
        bool   isEnabled;

        #endregion

        #region Commands

        public DelegateCommand SignUpButtonTapCommand { get; private set; }

        #endregion

        public UserSignUpViewModel(INavigationService navigationService) : base(navigationService)
        {
            SignUpButtonTapCommand = new DelegateCommand(RegistrationNewUser, CanExecute);
            Title = "Users SignUp";
        }

        

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

        public string UserConfirmPassword
        {
            get => confirmUserPassword;
            set => SetProperty(ref confirmUserPassword, value);
        }


        public bool IsEnabled 
        {
            get => isEnabled;
            set => SetProperty(ref isEnabled, value);
        }

        #endregion


        #region private helpers

        private async void RegistrationNewUser()
        {
            Authentification authentification = new Authentification();
            CodeUserAuthResult result = await authentification.IsAuthentication(UserLogin, UserPassword, UserConfirmPassword);

            switch (result)
            {
                case CodeUserAuthResult.InvalidLogin:
                    UserDialogs.Instance.Alert(Constants.INVALID_LOGIN);
                    UserLogin = "";
                    UserPassword = "";
                    UserConfirmPassword = "";
                    break;
                case CodeUserAuthResult.InvalidPassword:
                    UserDialogs.Instance.Alert(Constants.INVALID_PASSWORD);
                    UserPassword = "";
                    UserConfirmPassword = "";
                    break;
                case CodeUserAuthResult.PasswordMismatch:
                    UserDialogs.Instance.Alert(Constants.PASSWORD_MISMATCH);
                    UserPassword = "";
                    UserConfirmPassword = "";
                    break;
                case CodeUserAuthResult.LoginTaken:
                    UserDialogs.Instance.Alert(Constants.LOGIN_TAKEN);
                    UserLogin = "";
                    UserPassword = "";
                    UserConfirmPassword = "";
                    break;
                case CodeUserAuthResult.Passed:
                    UserDialogs.Instance.Alert(Constants.AUTHETICATION_SUCCESS);
                    UserRepository userRepository = new UserRepository();
                    await userRepository.SaveUserAsync(new Models.User
                    {
                        Login = UserLogin,
                        Password = UserPassword
                    });

                    NavigationParameters parameter = new NavigationParameters
                    {
                        { "login", UserLogin }
                    };
                    await NavigationService.NavigateAsync(nameof(UserSignIn), parameter);
                    break;
            }
        }


        private bool CanExecute() => IsEnabled;

        #endregion
    }
}