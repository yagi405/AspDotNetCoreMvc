using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ChatApp.Models.Managers.Imp
{
    public class PasswordManager : IPasswordManager
    {
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
    }
}
