namespace ProfileBook.ServiceData.Constants
{
    public static class Constants
    {
        #region User authentication

        public const string INVALID_LOGIN = "Invalid login.\nLogin must be from 4 to 16 characters and must not start with a number";
        public const string INVALID_PASSWORD = "Invalid password.\nPassword must be from 8 to 16 characters, must contain at least one uppercase letter, one lowercase and one number";
        public const string PASSWORD_MISMATCH = "Password mismatch.\nRe-enter passwords";
        public const string LOGIN_TAKEN = "This login is already taken.\nCome up with another";
        public const string AUTHETICATION_SUCCESS = "Success!!\nAccount created";
        public const string INVALID_LOGIN_OR_PASSWORD = "Invalid login or password!";

        #endregion


        #region Login and password verification

        public const int MIN_LENGTH_LOGIN = 4;
        public const int MIN_LENGTH_PASSWORD = 8;
        public const int MAX_LENGTH = 16;

        #endregion

        public const string PATH_TO_DEFAULT_IMAGE_PROFILE = "pic_profile.png";
    }
}