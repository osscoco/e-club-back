using DomainModels.Entities.Common;
using MediatR;

namespace E_Club.Application.Features.Clubs.GetClubById
{
    public record GetClubByIdQuery(Guid clubId) : IRequest<ResponseApi<object>>;
}
