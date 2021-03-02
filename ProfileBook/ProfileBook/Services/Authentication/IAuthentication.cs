using ProfileBook.ServiceData.Enums;
using System.Threading.Tasks;

namespace ProfileBook.Services.Authentication
{
    public interface IAuthentication
    {
        Task<CodeUserAuthResult> IsAuthentication(string login, string password, string confirmPassword);
    }
}