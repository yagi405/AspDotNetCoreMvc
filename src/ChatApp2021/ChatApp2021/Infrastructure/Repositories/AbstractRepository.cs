using System.Data;

namespace ChatApp2021.Infrastructure.Repositories
{
    public class AbstractRepository
    {
        private readonly IDbConnection _dbConnection;

        protected IDbConnection Connection => GetConnection();

        protected AbstractRepository(IDbConnection dbConnection)
        {
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
