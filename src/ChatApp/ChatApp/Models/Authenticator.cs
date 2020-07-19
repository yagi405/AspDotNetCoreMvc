using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using ChatApp.Common;
using ChatApp.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ChatApp.Models
{
    public class Authenticator : IAuthenticator
    {
        private const int SaltSize = 24;

        private readonly IPasswordManager _passwordManager;
        private readonly IUserRepository _userRepository;

        public Authenticator(IPasswordManager passwordManager, IUserRepository userRepository)
        {
            _passwordManager = passwordManager;
            _userRepository = userRepository;
        }

        public IIdentity Authenticate(string userId, string password)
        {
            return Authenticate(userId, password, out _);
        }

        public IIdentity Authenticate(string userId, string password, out ChatAppUser user)
        {
            var entity = _userRepository.GetById(userId);
            user = entity != null
                ? new ChatAppUser(entity.UserId, entity.UserName, entity.PasswordType, entity.PasswordSalt, entity.Password, entity.IsAdministrator)
                : null;

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
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            if (user.IsAdministrator)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, RoleConst.Admin));
            }

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
