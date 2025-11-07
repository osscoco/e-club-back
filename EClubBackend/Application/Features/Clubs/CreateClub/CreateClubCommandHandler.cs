using DomainModels.Entities.Common;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using E_Club.Application.Interfaces.Clubs;

namespace E_Club.Application.Features.Clubs.CreateClub
{
    public class CreateClubCommandHandler : IRequestHandler<CreateClubCommand, ResponseApi<object>>
    {
        #region Attributs
        private readonly IClubService _clubService;
        #endregion

        #region Constructeur
        public CreateClubCommandHandler(IClubService clubService)
        {
            _clubService = clubService;
        }
        #endregion

        #region Handle
        public async Task<ResponseApi<object>> Handle(CreateClubCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await _clubService.CreateClub(command, cancellationToken);
            }
            catch (AutoMapperMappingException ex)
            {
                return new ResponseApi<object>(false, new { }, $"Erreur de mapping : {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                return new ResponseApi<object>(false, new { }, $"Erreur lors de la création du club en base de données : {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                return new ResponseApi<object>(false, new { }, $"Une erreur inattendue est survenue : {ex.Message}");
            }
        }
        #endregion
    }
}