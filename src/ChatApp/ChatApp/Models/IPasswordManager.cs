namespace ChatApp.Models
{
    public interface IPasswordManager
    {
        bool IsMatch(string plainPassword, string salt, string hashedPassword);

        (string salt, string hashedPassword) GenerateSaltAndHashedPassword(string plainTextPassword);
    }
}
