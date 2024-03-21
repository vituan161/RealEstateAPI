using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Models;

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(UserManager<AppUser> userManager,
                       SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                PasswordHash = model.Password,
                Email = model.Email,
                Profile = new Profile
                {
                    Name = model.Name,
                    Address = model.Address,
                    DoB = model.DoB,
                    Phone = model.Phone,
                    IdentiticationNumber = model.IdentiticationNumber,
                }
            };
            var result = await _userManager.CreateAsync(user, user.PasswordHash);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(
                userName: email, 
                password: password, 
                isPersistent: false, 
                lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok("You have successfully logged in");
            }
            return BadRequest("Error occured");
        }
    }
}
