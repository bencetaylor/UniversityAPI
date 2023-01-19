using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolDatabase.Model.DTO;
using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDTO userForRegistration)
        {
            if (_userManager.Users.Any(u => u.UserName == userForRegistration.Username || u.Email == userForRegistration.Email))
            {
                throw new ApplicationException("Username/Email already exists!");
            }
            var user = new ApplicationUser
            {
                UserName = userForRegistration.Username,
                Name = userForRegistration.Name,
                Email = userForRegistration.Email,
                NeptunId = userForRegistration.NeptunId,
                DateOfBirth = userForRegistration.DateOfBirth,
                Department = userForRegistration.Department != null ? userForRegistration.Department : "ismeretlen"
            };
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            return result.Succeeded ? StatusCode(201) : throw new ApplicationException("Registration failed!");
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(userLoginDTO.Username, userLoginDTO.Password, false, false);
            return result.Succeeded ? StatusCode(200) : throw new ApplicationException("Login failed!");
        }

        [HttpPost]
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
