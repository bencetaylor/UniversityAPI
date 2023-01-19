namespace SchoolDatabase.Services
{
    public interface IUserService
    {
        public Task InitRoles();
        public Task InitUsers();
    }
}
