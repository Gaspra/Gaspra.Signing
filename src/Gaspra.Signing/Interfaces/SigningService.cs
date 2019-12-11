namespace SecretSigning.Interfaces
{
    public interface SigningService
    {
        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);
        string Encrypt(string data);
        string Decrypt(string data);
    }
}
