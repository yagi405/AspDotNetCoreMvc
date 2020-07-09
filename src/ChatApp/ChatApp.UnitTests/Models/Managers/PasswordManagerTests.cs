using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace ChatApp.UnitTests.Models.Managers
{
    public class PasswordManagerTests
    {
        private const int SaltSize = 24;

        [Fact]
        public void FromPlainToHashedString()
        {
            const string input = "P@ssw0rd";

            var salt = GenerateSalt();
            var bytes = Encoding.UTF8.GetBytes(input + salt);
            using var sha = new SHA256CryptoServiceProvider();
            var hashed = sha.ComputeHash(bytes);
            var hashedString = Convert.ToBase64String(hashed);
            Debug.Print(hashedString);
        }

        private string GenerateSalt()
        {
            using var provider = new RNGCryptoServiceProvider();
            var salt = new byte[SaltSize];
            provider.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
    }
}
