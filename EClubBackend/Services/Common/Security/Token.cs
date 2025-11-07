using DomainModels.Entities.Common;

namespace E_Club.Services.Common.Security
{
    public class Token
    {
        private readonly List<RevokedToken> _revokedTokens = new List<RevokedToken>();

        public void RevokeToken(string token)
        {
            _revokedTokens.Add(new RevokedToken
            {
                Token = token,
                RevokedAt = DateTime.UtcNow
            });
        }

        public bool IsTokenRevoked(string token)
        {
            return _revokedTokens.Any(rt => rt.Token == token);
        }
    }
}