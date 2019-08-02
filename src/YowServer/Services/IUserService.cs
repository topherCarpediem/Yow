using System.Threading.Tasks;
using Yow.YowServer.Models;
using Yow.YowServer.RequestModels;

namespace Yow.YowServer.Services
{
    public interface IUserService
    {
        Task<bool> IsEmailExist(string email);
        Task<User> GetUserByEmail(string email);
        Task<bool> IsEmailPasswordMatch(string email, string password);
        Task<bool> CreateNewUser(NewUser newUser, byte[] publicKey, byte[] privateKey);
    }
}