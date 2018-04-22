namespace kms.Services
{
    public interface IPasswordHasher
    {
        byte[] GenerateSalt();
        string GetHash(string password, byte[] salt);
    }
}
