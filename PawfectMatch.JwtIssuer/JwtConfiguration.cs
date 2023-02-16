using PawfectMatch.JwtIssuer.Interface;

namespace PawfectMatch.JwtIssuer
{
    public class JwtConfiguration : IJwtConfiguration
    {
        public JwtConfiguration(string issuer, string audience, string encryptionKey)
        {
            Issuer = issuer;
            Audience = audience;
            EncryptionKey = encryptionKey;
        }

        public string Issuer { get ; set ; }
        public string Audience { get ; set ; }
        public string EncryptionKey { get ; set ; }


    }
}
