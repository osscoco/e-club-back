using DomainModels.Entities;
using E_Club.Application.DTOs.User.Response;
using E_Club.Application.DTOs.Users.Request;

namespace E_Club.Application.Interfaces.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDtoResponse?>> GetUsers(CancellationToken cancellationToken);
        Task<UserDtoResponse?> GetUserById(Guid userId, CancellationToken cancellationToken);
        Task<User?> CreateUser(User user, CancellationToken cancellationToken);
        Task<UserDtoResponse?> UpdateUser(Guid userId, UserDtoUpdateRequest userDtoUpdateRequest, CancellationToken cancellationToken);
        Task<Guid?> DeleteUserById(Guid userId, CancellationToken cancellationToken);
    }
}
