using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using ChatApp.Models.Entities;
using ChatApp.Models.Entities.DbEntities;
using ChatApp.Models.Managers;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ChatApp.Models.Services.Imp
{
    public class AuthService : IAuthService
    {
        private const int SaltSize = 24;

        private readonly IUserService _userService;
        private readonly IPasswordManager _passwordManager;

        public AuthService(IPasswordManager passwordManager, IUserService userService)
        {
            _userService = userService;
            _passwordManager = passwordManager;
        }

        public IIdentity Authenticate(string userId, string password, out User user)
        {
            user = _userService.GetById(userId);
            if (user == null)
            {
                return null;
            }

            switch (user.PasswordType)
            {
                case PasswordType.PlainText:
                    if (user.Password != password)
                    {
                        return null;
                    }
                    break;
                case PasswordType.Hashed:
                    if (!_passwordManager.IsMatch(password, user.PasswordSalt, user.Password))
                    {
                        return null;
                    }
                    break;
                default:
                    return null;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId),
                new Claim(ClaimTypes.Name, user.Name),
            };

            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            return identity;
        }

        public string GenerateSalt()
        {
            using var provider = new RNGCryptoServiceProvider();
            var salt = new byte[SaltSize];
            provider.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public (string salt, string hashedPassword) GenerateSaltAndHashedPassword(string plainTextPassword)
        {
            var salt = GenerateSalt();
            using var sha = new SHA256CryptoServiceProvider();
            var hashed = sha.ComputeHash(Encoding.UTF8.GetBytes(plainTextPassword + salt));
            return (salt, Convert.ToBase64String(hashed));
        }
    }
}
