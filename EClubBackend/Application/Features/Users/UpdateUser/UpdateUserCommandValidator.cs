using FluentValidation;
using Microsoft.EntityFrameworkCore;
using InfrastructureEFCore;

namespace E_Club.Application.Features.Users.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator(AppDbContext db)
        {
            RuleFor(x => x.userDtoUpdateRequest.FirstName)
                .NotEmpty().WithMessage("Prénom de l'utilisateur : Requis ...")
                .MaximumLength(50).WithMessage("Prénom de l'utilisateur : Caractères maximum (50) ...");

            RuleFor(x => x.userDtoUpdateRequest.LastName)
                .NotEmpty().WithMessage("Nom de l'utilisateur : Requis ...")
                .MaximumLength(50).WithMessage("Nom de l'utilisateur : Caractères maximum (50) ...");

            RuleFor(x => x.userDtoUpdateRequest.Age)
                .NotEmpty().WithMessage("Age de l'utilisateur : Requis ...");

            RuleFor(x => x.userDtoUpdateRequest.Email)
                .NotEmpty().WithMessage("Email de l'utilisateur : Requis ...")
                .EmailAddress().WithMessage("Email de l'utilisateur : Format invalide ...")
                .MaximumLength(50).WithMessage("Email de l'utilisateur : Caractères maximum (50) ...");

            RuleFor(x => x.userDtoUpdateRequest.PasswordHashed)
                .NotEmpty().WithMessage("Mot de passe de l'utilisateur : Requis ...")
                .MinimumLength(10).WithMessage("Mot de passe de l'utilisateur : Caractères minimum (10) ...")
                .MaximumLength(255).WithMessage("Mot de passe de l'utilisateur : Caractères maximum (255) ...");

            RuleFor(x => x.userDtoUpdateRequest.Phone)
                .NotEmpty().WithMessage("Numéro de l'utilisateur : Requis ...")
                .MaximumLength(10).WithMessage("Numéro de l'utilisateur : Caractères maximum (10) ...");

            RuleFor(x => x.userDtoUpdateRequest.ClubId)
                .NotEmpty().WithMessage("Identifiant du club : Requis ...")
                .MustAsync(async (clubId, cancellation) =>
                await db.Clubs.AsNoTracking().AnyAsync(c => c.ClubId == clubId, cancellation))
                .WithMessage(club => $"Aucun club trouvé avec l'identifiant {club.userDtoUpdateRequest.ClubId} ...");

            RuleFor(x => x.userDtoUpdateRequest.UserTypeId)
                .NotEmpty().WithMessage("Identifiant du type d'utilisateur : Requis ...")
                .MustAsync(async (userTypeId, cancellation) =>
                await db.UserTypes.AsNoTracking().AnyAsync(c => c.UserTypeId == userTypeId, cancellation))
                .WithMessage(club => $"Aucun type d'utilisateur trouvé avec l'identifiant {club.userDtoUpdateRequest.UserTypeId} ...");
        }
    }
}
