using E_Club.Application.Interfaces.Auth;
using DomainModels.Entities.Common;
using MediatR;

namespace E_Club.Application.Features.Auth.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ResponseApi<object>>
    {
        #region Attributs        
        private readonly IAuthService _authService;
        #endregion

        #region Constructeur
        public LogoutCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        #endregion

        #region Handle
        public async Task<ResponseApi<object>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return _authService.Logout(request);
            }
            catch (Exception ex)
            {
                return new ResponseApi<object>(false, new { }, $"Erreur lors du chargement : {ex.Message}");
            }
        }
        #endregion
    }
}