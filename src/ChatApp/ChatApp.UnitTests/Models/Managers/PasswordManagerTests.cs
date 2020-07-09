using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace ChatApp.UnitTests.Models.Managers
{
    public class PasswordManagerTests
    {
        [Fact]
        public void FromPlainToHashedString()
        {
            const string input = "P@ssw0rd";

            var bytes = Encoding.UTF8.GetBytes(input);

            using var sha = new SHA256CryptoServiceProvider();
            var hashed = sha.ComputeHash(bytes);
            var hashedString = Convert.ToBase64String(hashed);
            Debug.Print(hashedString);
        }
    }
}
