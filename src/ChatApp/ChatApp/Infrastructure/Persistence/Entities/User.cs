using ChatApp.Common;

namespace ChatApp.Infrastructure.Persistence.Entities
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public PasswordType PasswordType { get; set; }
        public string PasswordSalt { get; set; }
        public string Password { get; set; }
        public bool IsAdministrator { get; set; }
        public string IconUrl { get; set; }

        public User(string userId, string userName, PasswordType passwordType, string passwordSalt, string password, bool isAdministrator, string iconUrl)
        {
            Args.NotEmpty(userId, nameof(userId));
            Args.NotEmpty(userName, nameof(userName));
            (UserId, UserName, PasswordType, PasswordSalt, Password, IsAdministrator, IconUrl) =
                (userId, userName, passwordType, passwordSalt, password, isAdministrator, iconUrl);
        }
    }
}
