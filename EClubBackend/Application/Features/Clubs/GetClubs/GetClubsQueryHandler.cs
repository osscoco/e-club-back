using DomainModels.Entities.Common;
using MediatR;
using E_Club.Application.Interfaces.Clubs;

namespace E_Club.Application.Features.Clubs.GetClubs
{
    public class GetClubsQueryHandler : IRequestHandler<GetClubsQuery, ResponseApi<object>>
    {
        #region Attributs
        private readonly IClubService _clubService;
        #endregion

        #region Constructeur
        public GetClubsQueryHandler(IClubService clubService)
        {
            _clubService = clubService;
        }
        #endregion

        #region Handle
        public async Task<ResponseApi<object>> Handle(GetClubsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _clubService.GetClubs(cancellationToken);
            }
            catch (Exception ex)
            {
                return new ResponseApi<object>(false, new { }, $"Erreur lors du chargement : {ex.Message}");
            }
        }
        #endregion
    }
}