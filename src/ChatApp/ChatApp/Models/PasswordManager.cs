using System;
using System.Linq;
using System.Security.Cryptography;

namespace ChatApp.Models
{
    public class PasswordManager : IPasswordManager
    {
        private const int SaltSize = 24;
        private const int Iterations = 10000;
        private const int HashSize = 32;

        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;

        public bool IsMatch(string plainTextPassword, string salt, string hashedPassword)
        {
            using var r = new Rfc2898DeriveBytes(plainTextPassword, Convert.FromBase64String(salt), Iterations, _hashAlgorithmName);
            return Convert.FromBase64String(hashedPassword).SequenceEqual(r.GetBytes(HashSize));
        }

        public (string salt, string hashedPassword) GenerateSaltAndHashedPassword(string plainTextPassword)
        {
            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(plainTextPassword, SaltSize, Iterations, _hashAlgorithmName);
            var hashedPassword = rfc2898DeriveBytes.GetBytes(HashSize);
            var salt = rfc2898DeriveBytes.Salt;
            return (Convert.ToBase64String(salt), Convert.ToBase64String(hashedPassword));
        }
    }
}