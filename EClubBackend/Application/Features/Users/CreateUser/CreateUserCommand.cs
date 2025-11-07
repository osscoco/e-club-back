using E_Club.Application.DTOs.Users.Request;
using DomainModels.Entities.Common;
using MediatR;

namespace E_Club.Application.Features.Users.CreateUser
{
    public record CreateUserCommand(UserDtoCreateRequest userDtoCreateRequest) : IRequest<ResponseApi<object>>;
}