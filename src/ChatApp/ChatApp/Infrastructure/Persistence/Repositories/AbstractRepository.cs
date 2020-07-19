using System.Data;
using ChatApp.Common;

namespace ChatApp.Infrastructure.Persistence.Repositories
{
    public abstract class AbstractRepository
    {
        private readonly IDbConnection _dbConnection;

        protected IDbConnection Connection => GetConnection();

        protected AbstractRepository(IDbConnection dbConnection)
        {
            Args.NotNull(dbConnection, nameof(dbConnection));
            _dbConnection = dbConnection;
        }

        private IDbConnection GetConnection()
        {
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }
            return _dbConnection;
        }
    }
}
