using DomainModels.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using E_Club.Application.Interfaces.Users;

namespace E_Club.Application.Features.Users.DeleteUserById
{
    public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, ResponseApi<object>>
    {
        #region Attributs
        private readonly IUserService _userService;
        #endregion

        #region Constructeur
        public DeleteUserByIdCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Handle
        public async Task<ResponseApi<object>> Handle(DeleteUserByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await _userService.DeleteUserById(command, cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                return new ResponseApi<object>(false, new { }, $"Impossible de supprimer cet utilisateur (il est peut-être référencé ailleurs) : {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                return new ResponseApi<object>(false, new { }, $"Erreur lors de la suppression : {ex.Message}");
            }
        }
        #endregion
    }
}