using ProfileBook.Models;
using ProfileBook.ServiceData.Enums;
using ProfileBook.Services.Repository;
using ProfileBook.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileBook.Services.Authorization
{
    public class Authorization : IAuthorization
    {
        public async Task<bool> IsAuthorization(string login, string password)
        {
            Task<bool> t1 = Task.Run(() => UserDataValidator.IsDataValid(login, CheckedItem.Login));
            Task<bool> t2 = Task.Run(() => UserDataValidator.IsDataValid(password, CheckedItem.Password));
            await Task.WhenAll(new[] { t1, t2 });

            if (!(t1.Result && t2.Result))
                return false;

            UserRepository userRepository = new UserRepository();
            List<User> users = await userRepository.GetUsersAsync;
            return users.Any(s => s.Login == login && s.Password == password);
        }
    }
}