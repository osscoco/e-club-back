using AutoMapper;
using DomainModels.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using E_Club.Application.Interfaces.Users;

namespace E_Club.Application.Features.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseApi<object>>
    {
        #region Attributs
        private readonly IUserService _userService;
        #endregion

        #region Constructeur
        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Handle
        public async Task<ResponseApi<object>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await _userService.CreateUser(command, cancellationToken);
            }
            catch (AutoMapperMappingException ex)
            {
                return new ResponseApi<object>(false, new { }, $"Erreur de mapping : {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                return new ResponseApi<object>(false, new { }, $"Erreur lors de la création de l'utilisateur en base de données : {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                return new ResponseApi<object>(false, new { }, $"Une erreur inattendue est survenue : {ex.Message}");
            }
        }
        #endregion
    }
}