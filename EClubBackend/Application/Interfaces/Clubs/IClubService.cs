using E_Club.Application.Features.Clubs.CreateClub;
using E_Club.Application.Features.Clubs.DeleteClubById;
using E_Club.Application.Features.Clubs.GetClubById;
using E_Club.Application.Features.Clubs.UpdateClub;
using DomainModels.Entities.Common;

namespace E_Club.Application.Interfaces.Clubs
{
    public interface IClubService
    {
        Task<ResponseApi<object>> GetClubs(CancellationToken cancellationToken);
        Task<ResponseApi<object>> GetClubById(GetClubByIdQuery query, CancellationToken cancellationToken);
        Task<ResponseApi<object>> CreateClub(CreateClubCommand command, CancellationToken cancellationToken);
        Task<ResponseApi<object>> UpdateClub(UpdateClubCommand command, CancellationToken cancellationToken);
        Task<ResponseApi<object>> DeleteClubById(DeleteClubByIdCommand command, CancellationToken cancellationToken);
    }
}
