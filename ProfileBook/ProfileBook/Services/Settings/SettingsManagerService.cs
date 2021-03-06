﻿using System;
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

        public string SortListBy
        {
            get => Preferences.Get(nameof(SortListBy), "");
            set => Preferences.Set(nameof(SortListBy), value);
        }

        // Has the sort changed
        public bool ChangeSort
        {
            get => Preferences.Get(nameof(ChangeSort), false);
            set => Preferences.Set(nameof(ChangeSort), value);
        }
    }
}