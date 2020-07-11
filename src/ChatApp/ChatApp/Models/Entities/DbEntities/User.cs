namespace ChatApp.Models.Entities.DbEntities
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public PasswordType PasswordType { get; set; }
        public string PasswordSalt { get; set; }
        public string Password { get; set; }
        public bool IsAdministrator { get; set; }

        public User(string userId, string userName, PasswordType passwordType, string passwordSalt, string password, bool isAdministrator)
        {
            UserId = userId;
            UserName = userName;
            PasswordType = passwordType;
            PasswordSalt = passwordSalt;
            Password = password;
            IsAdministrator = isAdministrator;
        }
    }
}
