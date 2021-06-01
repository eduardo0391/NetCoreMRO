namespace NetCoreReact.Context.Repositories
{
    public interface IUserRepository
    {
        User Add(User user);
        User userByName(string userName);
        string Encrypt(string stringToEncrypt);
        string Decrypt(string stringToDecrypt);
    }
}