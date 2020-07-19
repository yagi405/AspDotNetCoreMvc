using System;
using System.Data;
using ChatApp.Common;

namespace ChatApp.Infrastructure.Persistence.Repositories
{
    public static class DbCommandExtensions
    {
        public static void AddParameter<T>(this IDbCommand self, string parameterName, T value)
        {
            Args.NotEmpty(parameterName, nameof(parameterName));
            var param = self.CreateParameter();
            param.ParameterName = parameterName;
            if (value == null)
            {
                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }
            self.Parameters.Add(param);
        }
    }
}
