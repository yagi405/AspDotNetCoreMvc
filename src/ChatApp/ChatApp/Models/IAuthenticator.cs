using System.Security.Principal;

namespace ChatApp.Models
{
    public interface IAuthenticator
    {
        IIdentity Authenticate(string userId, string password);

        IIdentity Authenticate(string userId, string password, out ChatAppUser user);
    }
}
