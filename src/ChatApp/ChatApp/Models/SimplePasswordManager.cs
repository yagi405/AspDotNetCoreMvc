using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ChatApp.Models
{
    public class SimplePasswordManager : IPasswordManager
    {
        private const int SaltSize = 24;

        public bool IsMatch(string plainPassword, string salt, string hashedPassword)
        {
            if (string.IsNullOrEmpty(plainPassword))
            {
                return false;
            }

            var bytes = Encoding.UTF8.GetBytes(plainPassword + salt);
            using var sha = new SHA256CryptoServiceProvider();
            var src = sha.ComputeHash(bytes);

            return src.SequenceEqual(Convert.FromBase64String(hashedPassword));
        }

        public (string salt, string hashedPassword) GenerateSaltAndHashedPassword(string plainTextPassword)
        {
            var salt = GenerateSalt();
            using var sha = new SHA256CryptoServiceProvider();
            var hashed = sha.ComputeHash(Encoding.UTF8.GetBytes(plainTextPassword + salt));
            return (salt, Convert.ToBase64String(hashed));
        }

        private static string GenerateSalt()
        {
            using var provider = new RNGCryptoServiceProvider();
            var salt = new byte[SaltSize];
            provider.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
    }
}
