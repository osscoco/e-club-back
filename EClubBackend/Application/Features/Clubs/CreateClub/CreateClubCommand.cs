using E_Club.Application.DTOs.Clubs.Request;
using DomainModels.Entities.Common;
using MediatR;

namespace E_Club.Application.Features.Clubs.CreateClub
{
    public record CreateClubCommand(ClubDtoCreateRequest clubDtoCreateRequest) : IRequest<ResponseApi<object>>;
}