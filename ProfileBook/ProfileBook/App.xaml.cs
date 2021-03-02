using Prism;
using Prism.Ioc;
using Prism.Unity;
using ProfileBook.View;
using ProfileBook.Views;
using ProfileBook.ViewModels;
using System;
using System.IO;
using ProfileBook.Services.Settings;
using ProfileBook.ServiceData.Constants;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
        #region Fields

        private static Data.DbConnection database;
        private ISettingsManagerService settingsManager;

        #endregion

        #region Properties

        public static Data.DbConnection Database
        {
            get
            {
                if (database == null)
                    database = 
                        new Data.DbConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.DATABASE_NAME));
                return database;
            }
        }

        public ISettingsManagerService SettingsManager => settingsManager ??= Container.Resolve<SettingsManagerService>();

        #endregion

        public App(IPlatformInitializer initializer = null) : base(initializer) { }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry) 
        {
            #region services
            containerRegistry.RegisterInstance<ISettingsManagerService>(Container.Resolve<SettingsManagerService>());
            #endregion


            #region navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<UserSignIn, UserSignInViewModel>();
            containerRegistry.RegisterForNavigation<UserSignUp, UserSignUpViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewModel>();
            #endregion
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            string loggedUser = SettingsManager.LoggedUser;
            if (string.IsNullOrEmpty(loggedUser))
                NavigationService.NavigateAsync("NavigationPage/UserSignIn");
            else
                NavigationService.NavigateAsync("NavigationPage/MainListView");
        }
    }
}