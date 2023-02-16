namespace PawfectMatch.JwtIssuer.Interface
{
    public interface IJwtConfiguration
    {
        string Issuer { get; set; }
        string Audience { get; set; }
        string EncryptionKey { get; set; }
    }
}
