using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yow.YowServer.Models;
using Yow.YowServer.RequestModels;

namespace Yow.YowServer.Services
{
    public class UserService : IUserService
    {
        private readonly YowDbContext _context;

        public UserService(YowDbContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateNewUser(NewUser newUser, byte[] publicKey, byte[] privateKey)
        {

            var user = new User {
                Email = newUser.Email,
                Name = newUser.Name,
                Password = newUser.Password,
                KeyVault = new KeyVault {
                    PublicKey = publicKey,
                    SecretKey = privateKey
                }
            };

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return true;
        }

        public Task<User> GetUserByEmail(string email)
        {
            return _context.Users
                .Include(x => x.KeyVault)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public Task<bool> IsEmailExist(string email)
        {
            return _context.Users.AnyAsync(x => x.Email == email);
        }

        public Task<bool> IsEmailPasswordMatch(string email, string password)
        {
            return _context.Users.AnyAsync(x => x.Email == email && x.Password == password);
        }
    }
}