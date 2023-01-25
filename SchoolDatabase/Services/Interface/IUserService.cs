namespace SchoolDatabase.Services.Interface
{
    public interface IUserService
    {
        public Task InitRoles();
        public Task InitUsers();
    }
}
