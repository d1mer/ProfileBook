using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Services.Settings;

namespace ProfileBook.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize
    {
        protected INavigationService NavigationService { get;}
        protected ISettingsManagerService SettingsManager { get; set; }

        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public ViewModelBase(INavigationService navigationService) => NavigationService = navigationService;

        public virtual void Initialize(INavigationParameters parameters)
        {
            
        }
    }
}