using System.Security.Claims;
using System.Security.Principal;
using ChatApp.Models.Entities;
using ChatApp.Models.Managers;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ChatApp.Models.Services.Imp
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IPasswordManager _passwordManager;

        public AuthenticationService(IPasswordManager passwordManager, IUserService userService)
        {
            _userService = userService;
            _passwordManager = passwordManager;
        }

        public IIdentity Authenticate(string userId, string password)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            var user = _userService.GetById(userId);
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
                    if (!_passwordManager.IsMatch(password, user.Password))
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
    }
}
