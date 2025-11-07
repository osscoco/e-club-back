using DomainModels.Entities.Common;
using MediatR;

namespace E_Club.Application.Features.Auth.Logout
{
    public record LogoutCommand(HttpRequest HttpRequest) : IRequest<ResponseApi<object>>;
}