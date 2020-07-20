using System.Security.Claims;
using System.Security.Principal;
using ChatApp.Common;
using ChatApp.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ChatApp.Models
{
    public class Authenticator : IAuthenticator
    {
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
    }
}
