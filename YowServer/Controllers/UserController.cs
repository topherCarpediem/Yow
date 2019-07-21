using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yow.YowServer.Models;
using Yow.YowServer.RequestModels;
using Yow.YowServer.Services;

namespace Yow.YowServer.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ICryptoService _cryptoService;
        private readonly IUserService _userService;

        public UserController(ICryptoService cryptoService,
            IUserService userService)
        {
            _cryptoService = cryptoService ?? throw new System.ArgumentNullException(nameof(cryptoService));
            _userService = userService ?? throw new System.ArgumentNullException(nameof(userService));
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] NewUser newUser)
        {

            bool isExist = await  _userService.IsEmailExist(newUser.Email);

            if (isExist)
            {
                return BadRequest(new { Message = "User already exist" });
            }

            newUser.Password = _cryptoService.SecurePassword(newUser.Password);

            var keyVault = _cryptoService.GenerateKeyValuePair();

            await _userService.CreateNewUser(newUser, keyVault["publicKey"], keyVault["secretKey"]);
            
            return Ok( new { Message = "Successful registration" });
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] UserCredential userCredential)
        {
            
            User user = await  _userService.GetUserByEmail(userCredential.Email);

            if (user == null)
            {
                return BadRequest(new { Message = "Failure to login, email and password not match" });
            } 

            if (!_cryptoService.VerifyPassword(user.Password, userCredential.Password))
            {
                return BadRequest(new { Message = "Failure to login, email and password not match" });
            }
            
            return Ok( new { secretKey = user.KeyVault.SecretKey, publicKey = user.KeyVault.PublicKey });
        }

    }
}