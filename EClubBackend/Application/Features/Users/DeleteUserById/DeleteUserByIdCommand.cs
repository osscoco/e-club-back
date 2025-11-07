using DomainModels.Entities.Common;
using MediatR;

namespace E_Club.Application.Features.Users.DeleteUserById
{
    public record DeleteUserByIdCommand(Guid userId) : IRequest<ResponseApi<object>>;
}
