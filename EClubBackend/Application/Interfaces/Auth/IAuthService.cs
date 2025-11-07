using E_Club.Application.Features.Auth.AuthMe;
using E_Club.Application.Features.Auth.Login;
using E_Club.Application.Features.Auth.Logout;
using DomainModels.Entities.Common;

namespace E_Club.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<ResponseApi<object>> Login(LoginCommand request, CancellationToken cancellationToken);
        Task<ResponseApi<object>> AuthMe(AuthMeCommand request, CancellationToken cancellationToken);
        ResponseApi<object> Logout(LogoutCommand request);
    }
}
