using DomainModels.Entities.Common;
using MediatR;
using E_Club.Application.Interfaces.Clubs;

namespace E_Club.Application.Features.Clubs.GetClubById
{
    public class GetClubByIdQueryHandler : IRequestHandler<GetClubByIdQuery, ResponseApi<object>>
    {
        #region Attributs
        private readonly IClubService _clubService;
        #endregion

        #region Constructeur
        public GetClubByIdQueryHandler(IClubService clubService)
        {
            _clubService = clubService;
        }
        #endregion

        #region Handle
        public async Task<ResponseApi<object>> Handle(GetClubByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                return await _clubService.GetClubById(query, cancellationToken);
            }
            catch (Exception ex)
            {
                return new ResponseApi<object>(false, new { }, $"Erreur lors du chargement : {ex.Message}");
            }
        }
        #endregion
    }
}