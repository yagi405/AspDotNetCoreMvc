﻿using System.Data;

namespace ChatApp.Models.Services
{
    public abstract class AbstractDbService
    {
        private readonly IDbConnection _dbConnection;

        protected IDbConnection Connection => GetConnection();

        protected AbstractDbService(IDbConnection dbConnection)
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
