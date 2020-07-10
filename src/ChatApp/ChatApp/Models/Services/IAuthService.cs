using System.Security.Principal;
using ChatApp.Models.Entities.DbEntities;

namespace ChatApp.Models.Services
{
    public interface IAuthService
    {
        IIdentity Authenticate(string userId, string password, out User user);

        (string salt, string hashedPassword) GenerateSaltAndHashedPassword(string plainTextPassword);
    }
}
