namespace ChatApp.Models.Managers
{
    public interface IPasswordManager
    {
        bool IsMatch(string plain, string hashed);
    }
}
