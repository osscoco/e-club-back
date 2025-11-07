using DomainModels.Entities.Common;
using MediatR;
using E_Club.Application.Interfaces.Users;

namespace E_Club.Application.Features.Users.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResponseApi<object>>
    {
        #region Attributs
        private readonly IUserService _userService;
        #endregion

        #region Constructeur
        public GetUserByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Handle
        public async Task<ResponseApi<object>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                return await _userService.GetUserById(query, cancellationToken);
            }
            catch (Exception ex)
            {
                return new ResponseApi<object>(false, new { }, $"Erreur lors du chargement : {ex.Message}");
            }
        }
        #endregion
    }
}