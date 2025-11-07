using DomainModels.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using E_Club.Application.Interfaces.Clubs;

namespace E_Club.Application.Features.Clubs.UpdateClub
{
    public class UpdateClubCommandHandler : IRequestHandler<UpdateClubCommand, ResponseApi<object>>
    {
        #region Attributs
        private readonly IClubService _clubService;
        #endregion

        #region Constructeur
        public UpdateClubCommandHandler(IClubService clubService)
        {
            _clubService = clubService;
        }
        #endregion

        #region Handle
        public async Task<ResponseApi<object>> Handle(UpdateClubCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await _clubService.UpdateClub(command, cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ResponseApi<object>(false, new { }, "Erreur de concurrence : le club a été modifié entre-temps.");
            }
            catch (Exception ex)
            {
                return new ResponseApi<object>(false, new { }, $"Erreur lors de la mise à jour : {ex.Message}");
            }
        }
        #endregion
    }
}