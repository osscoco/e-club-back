using DomainModels.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using E_Club.Application.Interfaces.Users;

namespace E_Club.Application.Features.Users.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResponseApi<object>>
    {
        #region Attributs
        private readonly IUserService _userService;
        #endregion

        #region Constructeur
        public UpdateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Handle
        public async Task<ResponseApi<object>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await _userService.UpdateUser(command, cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ResponseApi<object>(false, new { }, "Erreur de concurrence : l'utilisateur a été modifié entre-temps.");
            }
            catch (Exception ex)
            {
                return new ResponseApi<object>(false, new { }, $"Erreur lors de la mise à jour : {ex.Message}");
            }
        }
        #endregion
    }
}