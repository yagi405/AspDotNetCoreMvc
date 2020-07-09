using System;
using System.Security.Cryptography;
using System.Text;

namespace ChatApp.Models.Managers.Imp
{
    public class PasswordManager : IPasswordManager
    {
        public bool IsMatch(string plain, string hashed)
        {
            if (string.IsNullOrEmpty(plain))
            {
                return false;
            }

            var bytes = Encoding.UTF8.GetBytes(plain);

            using var sha = new SHA256CryptoServiceProvider();
            var hashedBytes = sha.ComputeHash(bytes);
            var hashedString = Convert.ToBase64String(hashedBytes);

            return hashedString == hashed;
        }
    }
}
