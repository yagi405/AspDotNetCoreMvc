using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ChatApp.Models.Services.Imp
{
    public class AuthenticationService : IAuthenticationService
    {
        public IIdentity Authenticate(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return null;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
            };

            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            return identity;
        }
    }
}
