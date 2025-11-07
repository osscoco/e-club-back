using DomainModels.Entities.Common;
using MediatR;

namespace E_Club.Application.Features.Users.GetUserById
{
    public record GetUserByIdQuery(Guid userId) : IRequest<ResponseApi<object>>;
}