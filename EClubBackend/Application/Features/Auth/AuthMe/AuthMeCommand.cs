using DomainModels.Entities.Common;
using MediatR;
using System.Security.Claims;

namespace E_Club.Application.Features.Auth.AuthMe
{
    public record AuthMeCommand(ClaimsPrincipal user) : IRequest<ResponseApi<object>>;
}
