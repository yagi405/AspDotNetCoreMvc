using System.Collections.Generic;
using ChatApp.Infrastructure.Persistence.Entities;

namespace ChatApp.Infrastructure.Persistence.Repositories
{
    public interface IUserRepository
    {
        User GetById(string userId);

        IList<User> GetAll();

        void ChangePassword(string userId, string salt, string hashedPassword);

        void ChangeUserName(string userId, string userName);

        void ChangeUserIcon(string userId, string iconUrl);

        User Create(User user);

        void Edit(string userId, string userName, bool isAdministrator);

        void Delete(string userId);

    }
}
