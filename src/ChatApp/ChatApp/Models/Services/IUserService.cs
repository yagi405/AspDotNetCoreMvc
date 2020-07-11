using System.Collections.Generic;
using ChatApp.Models.Entities.DbEntities;

namespace ChatApp.Models.Services
{
    public interface IUserService
    {
        User GetById(string userId);

        IList<User> GetAll();

        bool ChangePassword(User user, string salt, string hashedPassword);

        User Create(User user);

        bool Edit(string userId, string userName, bool isAdministrator);

        bool Delete(string userId);
    }
}
