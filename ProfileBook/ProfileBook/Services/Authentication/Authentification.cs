using ProfileBook.ServiceData.Enums;
using ProfileBook.Validators;
using System.Threading.Tasks;
using ProfileBook.Services.Repository;
using ProfileBook.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProfileBook.Services.Authentication
{
    public class Authentification : IAuthentication
    {
        public async Task<CodeUserAuthResult> IsAuthentication(string login, string password, string confirmPassword)
        {
            bool result = await Task.Run(() => UserDataValidator.IsDataValid(login, CheckedItem.Login));
            if (!result)
                return CodeUserAuthResult.InvalidLogin;

            result = password == confirmPassword;
            if (!result)
                return CodeUserAuthResult.PasswordMismatch;

            result = await Task.Run(() => UserDataValidator.IsDataValid(password, CheckedItem.Password));
            if (!result)
                return CodeUserAuthResult.InvalidPassword;

            UserRepository userRepository = new UserRepository();
            List<User> users = await userRepository.GetUsersAsync;
            result = users.Any(s => s.Login == login);
            if (result)
                return CodeUserAuthResult.LoginTaken;

            return CodeUserAuthResult.Passed;
        }
    }
}