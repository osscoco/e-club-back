using E_Club.Application.DTOs.Users.Request;
using DomainModels.Entities.Common;
using MediatR;

namespace E_Club.Application.Features.Users.UpdateUser
{
    public record UpdateUserCommand(Guid userId, UserDtoUpdateRequest userDtoUpdateRequest) : IRequest<ResponseApi<object>>;
}
