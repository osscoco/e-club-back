using DomainModels.Entities.Common;
using E_Club.Application.Features.Users.CreateUser;
using E_Club.Application.Features.Users.DeleteUserById;
using E_Club.Application.Features.Users.GetUserById;
using E_Club.Application.Features.Users.UpdateUser;

namespace E_Club.Application.Interfaces.Users
{
    public interface IUserService
    {
        Task<ResponseApi<object>> GetUsers(CancellationToken cancellationToken);
        Task<ResponseApi<object>> GetUserById(GetUserByIdQuery query, CancellationToken cancellationToken);
        Task<ResponseApi<object>> CreateUser(CreateUserCommand command, CancellationToken cancellationToken);
        Task<ResponseApi<object>> UpdateUser(UpdateUserCommand command, CancellationToken cancellationToken);
        Task<ResponseApi<object>> DeleteUserById(DeleteUserByIdCommand command, CancellationToken cancellationToken);
    }
}
