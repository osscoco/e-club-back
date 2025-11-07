using FluentValidation;
using Microsoft.EntityFrameworkCore;
using InfrastructureEFCore;

namespace E_Club.Application.Features.Clubs.DeleteClubById
{
    public class DeleteClubByIdCommandValidator : AbstractValidator<DeleteClubByIdCommand>
    {
        public DeleteClubByIdCommandValidator(AppDbContext db)
        {
            RuleFor(x => x.clubId)
                    .NotEmpty().WithMessage("Identifiant du club : Requis ...")
                    .MustAsync(async (clubId, cancellation) =>
                    await db.Clubs.AsNoTracking().AnyAsync(c => c.ClubId == clubId, cancellation))
                    .WithMessage(club => $"Aucun club trouvé avec l'identifiant {club.clubId} ...")
                    .MustAsync(async (clubId, cancellation) =>
                    !await db.Users.AsNoTracking().AnyAsync(u => u.ClubId == clubId, cancellation))
                    .WithMessage(club => $"Club {club.clubId} rattaché à au moins un utilisateur ...");
        }
    }
}