using E_Club.Application.DTOs.Auth.Response;

namespace E_Club.Application.Interfaces.Auth
{
    public interface IAuthRepository
    {
        Task<LoginDtoResponse?> GetUserByEmail(string email, CancellationToken cancellationToken);
        Task<AuthMeDtoResponse?> GetUserById(Guid userId, CancellationToken cancellationToken);
    }
}
