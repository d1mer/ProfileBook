using Prism.Commands;
using Prism.Navigation;
using System;
using Acr.UserDialogs;
using Xamarin.Essentials;
using ProfileBook.Services.Settings;
using ProfileBook.ServiceData.Constants;
using System.IO;
using ProfileBook.Services.DbService;
using ProfileBook.Models;

namespace ProfileBook.ViewModels
{
    public class AddEditProfileViewModel : ViewModelBase
    {
        public AddEditProfileViewModel(INavigationService navigationService, IDbService dbService, ISettingsManagerService settingsManager) : base(navigationService, dbService, settingsManager)
        {
            Title = "Add Profile";
        }


        #region Private fields

        private string pathToImageSourceProfile = Constants.PATH_TO_DEFAULT_IMAGE_PROFILE;
        private string nickName;
        private string name;
        private string description;

        #endregion


        #region Commands

        public DelegateCommand ImageTapCommand => new DelegateCommand(OpenActionSheet);
        public DelegateCommand SaveTapCommand => new DelegateCommand(SaveProfile);

        #endregion


        #region Properties

        public string PathToImageSourceProfile
        {
            get => pathToImageSourceProfile;
            set => SetProperty(ref pathToImageSourceProfile, value);
        }

        public string NickName
        {
            get => nickName;
            set => SetProperty(ref nickName, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }


        #endregion


        #region Private helpers

        private void OpenActionSheet()
        {
            UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                                             .SetTitle("Choose action")
                                             .Add("Open gallery", GetPhotoAsync, "ic_collections_black.png")
                                             .Add("Open camera", TakePhotoAsync, "ic_camera_alt_black.png"));
        }

        private async void GetPhotoAsync()
        {
            try
            {
                FileResult photo = await MediaPicker.PickPhotoAsync();
                PathToImageSourceProfile = photo.FullPath;
            }
            catch(Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message, "Error");
                pathToImageSourceProfile = Constants.PATH_TO_DEFAULT_IMAGE_PROFILE;
            }
                
        }

        private async void TakePhotoAsync()
        {
            try
            {
                FileResult photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"{SettingsManager.LoggedUser}-{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });

                string file = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (Stream stream = await photo.OpenReadAsync())
                using (FileStream newStream = File.OpenWrite(file))
                    await stream.CopyToAsync(newStream);

                PathToImageSourceProfile = file;
            }
            catch(Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message, "Error");
                pathToImageSourceProfile = Constants.PATH_TO_DEFAULT_IMAGE_PROFILE;
            }
        }


        private async void SaveProfile()
        {
            if(string.IsNullOrWhiteSpace(NickName) || string.IsNullOrWhiteSpace(Name))
            {
                UserDialogs.Instance.Alert("NickName and Name fields must be filled");
                return;
            }

            ProfileModel profile = new ProfileModel
            {
                NickName = NickName,
                Name = Name,
                ImagePath = PathToImageSourceProfile,
                Owner = SettingsManager.LoggedUser,
                CreationTime = DateTime.Now,
                Description = Description
            };

            try
            {
                int id = await DbService.InsertDataAsync(profile);
                profile.Id = id;
                UserDialogs.Instance.Alert("Saved");
                //ProfileList.Add(profile);
                await NavigationService.GoBackAsync();
            }
            catch(Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
        }

        #endregion
    }
}