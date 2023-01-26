using Microsoft.AspNetCore.Identity;
using SchoolDatabase.Model.DTO;

namespace SchoolDatabase.Services.Interface
{
    public interface IUserService
    {
        public Task<IdentityResult> RegisterUser(UserRegistrationDTO userForRegistration);
        public Task InitRoles();
        public Task InitUsers();
    }
}
