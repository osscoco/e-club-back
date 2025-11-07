using DomainModels.Entities.Common;
using MediatR;

namespace E_Club.Application.Features.Users.GetUsers
{
    public record GetUsersQuery() : IRequest<ResponseApi<object>>;
}
