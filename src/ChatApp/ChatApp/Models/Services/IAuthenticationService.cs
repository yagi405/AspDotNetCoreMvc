using System.Security.Principal;

namespace ChatApp.Models.Services
{
    public interface IAuthenticationService
    {
        IIdentity Authenticate(string userName);
    }
}
