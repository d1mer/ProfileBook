using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProfileBook;

namespace ProfileBook.Services.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task<List<User>> GetUsersAsync => App.Database.database.Table<User>().ToListAsync();

        public async Task<int> SaveUserAsync(User user) => await App.Database.database.InsertAsync(user);
    }
}