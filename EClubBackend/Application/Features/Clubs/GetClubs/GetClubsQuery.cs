using DomainModels.Entities.Common;
using MediatR;

namespace E_Club.Application.Features.Clubs.GetClubs
{
    public record GetClubsQuery() : IRequest<ResponseApi<object>>;
}
