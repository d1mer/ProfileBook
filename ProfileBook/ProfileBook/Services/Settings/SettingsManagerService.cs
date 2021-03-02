using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace ProfileBook.Services.Settings
{
    public class SettingsManagerService : ISettingsManagerService
    {
        public string LoggedUser 
        {
            get => Preferences.Get(nameof(LoggedUser), "");
            set => Preferences.Set(nameof(LoggedUser), value);
        }
    }
}