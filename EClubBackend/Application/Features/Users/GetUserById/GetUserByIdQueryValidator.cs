using FluentValidation;
using Microsoft.EntityFrameworkCore;
using InfrastructureEFCore;

namespace E_Club.Application.Features.Users.GetUserById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator(AppDbContext db) 
        {
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage("Identifiant de l'utilisateur : Requis ...")
                .MustAsync(async (userId, cancellation) =>
                await db.Users.AsNoTracking().AnyAsync(c => c.UserId == userId, cancellation))
                .WithMessage(user => $"Aucun utilisateur trouvé avec l'identifiant {user.userId} ...");
        }
    }
}
