﻿using ChatApp.Common;

namespace ChatApp.Models
{
    public class ChatAppUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public PasswordType PasswordType { get; set; }
        public string PasswordSalt { get; set; }
        public string Password { get; set; }
        public bool IsAdministrator { get; set; }

        public ChatAppUser(string userId, string userName, PasswordType passwordType, string passwordSalt, string password, bool isAdministrator)
        {
            Args.NotEmpty(userId, nameof(userId));
            Args.NotEmpty(userName, nameof(userName));
            (UserId, UserName, PasswordType, PasswordSalt, Password, IsAdministrator) =
                (userId, userName, passwordType, passwordSalt, password, isAdministrator);
        }
    }
}
