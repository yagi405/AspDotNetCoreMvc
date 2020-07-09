namespace ChatApp.Models.Entities.DbEntities
{
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public PasswordType PasswordType { get; set; }
        public string PasswordSalt { get; set; }
        public string Password { get; set; }
        public bool IsAdministrator { get; set; }

        public User(string userId, string name, PasswordType passwordType, string passwordSalt, string password, bool isAdministrator)
        {
            UserId = userId;
            Name = name;
            PasswordType = passwordType;
            PasswordSalt = passwordSalt;
            Password = password;
            IsAdministrator = isAdministrator;
        }
    }
}
