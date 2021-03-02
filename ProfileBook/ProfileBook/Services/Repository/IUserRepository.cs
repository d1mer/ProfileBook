using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync { get; }

        Task<int> SaveUserAsync(User user);
    }
}