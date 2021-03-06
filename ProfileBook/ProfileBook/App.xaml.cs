using Prism;
using Prism.Ioc;
using Prism.Unity;
using ProfileBook.View;
using ProfileBook.Views;
using ProfileBook.ViewModels;
using ProfileBook.Services.Settings;
using Xamarin.Forms;
using ProfileBook.Services.Repository;
using ProfileBook.Services.DbService;
using ProfileBook.Services.Registration;
using ProfileBook.Services.Authorization;
using ProfileBook.Dialogs;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
        #region Private fields

        private ISettingsManagerService settingsManager;
        private IRepository repository;
        private IDbService dbService;
        private IRegistration registrationService;
        private IAuthorization authorizationService;

        #endregion

        #region Properties

        public ISettingsManagerService SettingsManager => settingsManager ??= Container.Resolve<SettingsManagerService>();

        public IRepository Repository => repository ??= Container.Resolve<Repository>();

        public IDbService DbService => dbService ??= Container.Resolve<DbService>();

        public IRegistration RegistrationService => registrationService ??= Container.Resolve<Registration>();

        public IAuthorization AuthorizationService => authorizationService ??= Container.Resolve<Authorization>();

        #endregion


        public App(IPlatformInitializer initializer = null) : base(initializer) { }


        #region Overrides

        protected override void RegisterTypes(IContainerRegistry containerRegistry) 
        {
            #region services
            containerRegistry.RegisterInstance<ISettingsManagerService>(Container.Resolve<SettingsManagerService>());
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<IDbService>(Container.Resolve<DbService>());
            containerRegistry.RegisterInstance<IRegistration>(Container.Resolve<Registration>());
            containerRegistry.RegisterInstance<IAuthorization>(Container.Resolve<Authorization>());
            #endregion


            #region navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<UserSignIn, UserSignInViewModel>();
            containerRegistry.RegisterForNavigation<UserSignUp, UserSignUpViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            #endregion

            #region dialogs

            containerRegistry.RegisterDialog<ItemTappedDialog, ItemTappedDialogModel>();

            #endregion
        }


        #region Overrides

        protected override void OnInitialized()
        {
            InitializeComponent();

            string loggedUser = SettingsManager.LoggedUser;
            if (string.IsNullOrEmpty(loggedUser))
                NavigationService.NavigateAsync("NavigationPage/UserSignIn");
            else
                NavigationService.NavigateAsync("NavigationPage/MainListView");
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }

        #endregion

        #endregion
    }
}