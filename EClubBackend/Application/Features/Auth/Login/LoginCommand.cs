using E_Club.Application.DTOs.Auth.Request;
using DomainModels.Entities.Common;
using MediatR;

namespace E_Club.Application.Features.Auth.Login
{
    public record LoginCommand(LoginDtoRequest loginDtoRequest) : IRequest<ResponseApi<object>>;
}
