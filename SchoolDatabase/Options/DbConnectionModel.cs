using Microsoft.Extensions.Options;

namespace SchoolDatabase.Options
{
    public class DbConnectionModel
    {
        private readonly DbConnectionOption _connectionOptions;

        public DbConnectionModel(IOptions<DbConnectionOption> connectionOptions)
        {
            _connectionOptions = connectionOptions.Value;
        }

        public DbConnectionOption GetConnection()
        {
            return _connectionOptions;
        }
    }
}
