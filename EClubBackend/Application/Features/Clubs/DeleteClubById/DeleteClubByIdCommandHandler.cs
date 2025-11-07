using DomainModels.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using E_Club.Application.Interfaces.Clubs;

namespace E_Club.Application.Features.Clubs.DeleteClubById
{
    public class DeleteClubByIdCommandHandler : IRequestHandler<DeleteClubByIdCommand, ResponseApi<object>>
    {
        #region Attributs
        private readonly IClubService _clubService;
        #endregion

        #region Constructeur
        public DeleteClubByIdCommandHandler(IClubService clubService)
        {
            _clubService = clubService;
        }
        #endregion

        #region Handle
        public async Task<ResponseApi<object>> Handle(DeleteClubByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await _clubService.DeleteClubById(command, cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                return new ResponseApi<object>(false, new { }, $"Impossible de supprimer ce club (il est peut-être référencé ailleurs) : {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                return new ResponseApi<object>(false, new { }, $"Erreur lors de la suppression : {ex.Message}");
            }
        }
        #endregion
    }
}