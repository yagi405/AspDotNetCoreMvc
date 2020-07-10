using System.Data;

namespace ChatApp.Models.Extensions
{
    public static class DbCommandExtensions
    {
        public static void AddParameter<T>(this IDbCommand self, string parameterName, T value)
        {
            var param = self.CreateParameter();
            param.ParameterName = parameterName;
            param.Value = value;
            self.Parameters.Add(param);
        }
    }
}
