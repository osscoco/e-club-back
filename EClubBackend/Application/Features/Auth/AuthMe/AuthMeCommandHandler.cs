using E_Club.Application.Interfaces.Auth;
using DomainModels.Entities.Common;
using MediatR;

namespace E_Club.Application.Features.Auth.AuthMe
{
    public class AuthMeCommandHandler : IRequestHandler<AuthMeCommand, ResponseApi<object>>
    {
        #region Attributs        
        private readonly IAuthService _authService;
        #endregion

        #region Constructeur
        public AuthMeCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        #endregion

        #region Handle
        public async Task<ResponseApi<object>> Handle(AuthMeCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await _authService.AuthMe(command, cancellationToken);
            }
            catch (Exception ex)
            {
                return new ResponseApi<object>(false, new { }, $"Erreur lors du chargement : {ex.Message}");
            }
        }
        #endregion
    }
}