using DomainModels.Entities.Common;
using MediatR;

namespace E_Club.Application.Features.Clubs.DeleteClubById
{
    public record DeleteClubByIdCommand(Guid clubId) : IRequest<ResponseApi<object>>;
}