namespace ChatApp.Models.Managers
{
    public interface IPasswordManager
    {
        bool IsMatch(string plainPassword, string salt, string hashedPassword);
    }
}
