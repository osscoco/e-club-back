using E_Club.Application.DTOs.Clubs.Request;
using E_Club.Application.DTOs.Clubs.Response;
using DomainModels.Entities;

namespace E_Club.Application.Interfaces.Clubs
{
    public interface IClubRepository
    {
        Task<IEnumerable<ClubDtoResponse?>> GetClubs(CancellationToken cancellationToken);
        Task<ClubDtoResponse?> GetClubById(Guid clubId, CancellationToken cancellationToken);
        Task<Club?> CreateClub(Club club, CancellationToken cancellationToken);
        Task<ClubDtoResponse?> UpdateClub(Guid clubId, ClubDtoUpdateRequest clubDtoUpdateRequest, CancellationToken cancellationToken);
        Task<Guid?> DeleteClubById(Guid clubId, CancellationToken cancellationToken);
    }
}