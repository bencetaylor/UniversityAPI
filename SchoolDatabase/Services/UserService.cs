using Microsoft.AspNetCore.Identity;
using SchoolDatabase.Model.DTO;
using SchoolDatabase.Model.Entity;
using System.Security.Claims;

namespace SchoolDatabase.Services
{
    public class UserService : IUserService
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(RoleManager<IdentityRole<int>> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task InitRoles()
        {
            await CreateRole(Roles.ADMIN);
            await CreateRole(Roles.TEACHER);
            await CreateRole(Roles.STUDENT);
        }
        public async Task InitUsers()
        {
            await CreateUser("admin", "Admin", "admin@uni-pannon.hu", "ADMIN", new DateTime(1990, 1, 1), null, "Admin_1234", Roles.ADMIN);
            await CreateUser("rf57v5", "Szabó Bence", "rf57v5@uni-pannon.hu", "RF57V5", new DateTime(1990, 1, 1), null, "Teacher_1234", Roles.TEACHER);
            await CreateUser("abc123", "Teszt Elek", "abc123@uni-pannon.hu", "ABC123", new DateTime(2000, 1, 1), null, "User_1234", Roles.STUDENT);
        }

        private async Task CreateRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if(role == null)
            {
                await _roleManager.CreateAsync(new IdentityRole<int>(roleName));
            }
        }

        private async Task CreateUser(string userName, string name, string email, string neptunId, DateTime dateOfBirth,
                                      string? department, string password, string role)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = userName,
                Name = name,
                Email = email,
                NeptunId = neptunId,
                DateOfBirth = dateOfBirth,
                Department = department != null ? department : "ismeretlen"
            };
           
            var entity = await _userManager.CreateAsync(user, password);
            if (entity.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));
            }
        }

    }
}
