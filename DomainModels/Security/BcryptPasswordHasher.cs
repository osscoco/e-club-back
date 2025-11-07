using Microsoft.Extensions.Configuration;

namespace DomainModels.Security
{
    public class BcryptPasswordHasher : IPasswordHasher
    {
        #region Attributs
        private readonly string? _pepper;
        #endregion

        #region Constructeur
        public BcryptPasswordHasher(IConfiguration config)
        {
            _pepper = config["Security:PasswordPepper"];
        }
        #endregion

        #region Hash
        public string Hash(string password)
        {
            var input = password + _pepper;
            return BCrypt.Net.BCrypt.HashPassword(input, workFactor: 12);
        }
        #endregion

        #region Verify
        public bool Verify(string password, string hash)
        {
            var input = password + _pepper;
            return BCrypt.Net.BCrypt.Verify(input, hash);
        }
        #endregion
    }
}