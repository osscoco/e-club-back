using FluentValidation;
using Microsoft.EntityFrameworkCore;
using InfrastructureEFCore;

namespace E_Club.Application.Features.Clubs.GetClubById
{
    public class GetClubByIdQueryValidator : AbstractValidator<GetClubByIdQuery>
    {
        public GetClubByIdQueryValidator(AppDbContext db)
        {
            RuleFor(x => x.clubId)
                .NotEmpty().WithMessage("Identifiant du club : Requis ...")
                .MustAsync(async (clubId, cancellation) =>
                await db.Clubs.AsNoTracking().AnyAsync(c => c.ClubId == clubId, cancellation))
                .WithMessage(club => $"Aucun club trouvé avec l'identifiant {club.clubId} ...");
        }
    }
}
