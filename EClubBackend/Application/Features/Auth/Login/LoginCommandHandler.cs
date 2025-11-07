using E_Club.Application.Interfaces.Auth;
using DomainModels.Entities.Common;
using MediatR;

namespace E_Club.Application.Features.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseApi<object>>
    {
        #region Attributs
        private readonly IAuthService _authService;
        #endregion

        #region Constructeur
        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        #endregion

        #region Handle
        public async Task<ResponseApi<object>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _authService.Login(request, cancellationToken);
            }
            catch (Exception ex)
            {
                return new ResponseApi<object>(false, new { }, $"Erreur lors du chargement : {ex.Message}");
            }
        }
        #endregion
    }
}