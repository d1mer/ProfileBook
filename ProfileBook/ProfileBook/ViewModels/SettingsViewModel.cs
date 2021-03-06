using Prism.Navigation;
using ProfileBook.Services.DbService;
using ProfileBook.Services.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel(INavigationService navigationService, IDbService dbService, ISettingsManagerService settingsManager) : base(navigationService, dbService, settingsManager)
        {
            Title = "Settings";
        }
    }
}