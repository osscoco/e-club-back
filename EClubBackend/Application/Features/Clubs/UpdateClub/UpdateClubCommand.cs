using E_Club.Application.DTOs.Clubs.Request;
using DomainModels.Entities.Common;
using MediatR;

namespace E_Club.Application.Features.Clubs.UpdateClub
{
    public record UpdateClubCommand(Guid clubId, ClubDtoUpdateRequest clubDtoUpdateRequest) : IRequest<ResponseApi<object>>;
}
