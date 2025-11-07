using FluentValidation;
using Microsoft.EntityFrameworkCore;
using InfrastructureEFCore;

namespace E_Club.Application.Features.Users.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(AppDbContext db)
        {
            RuleFor(x => x.userDtoCreateRequest.FirstName)
                .NotEmpty().WithMessage("Prénom de l'utilisateur : Requis ...")
                .MaximumLength(50).WithMessage("Prénom de l'utilisateur : Caractères maximum (50) ...");

            RuleFor(x => x.userDtoCreateRequest.LastName)
                .NotEmpty().WithMessage("Nom de l'utilisateur : Requis ...")
                .MaximumLength(50).WithMessage("Nom de l'utilisateur : Caractères maximum (50) ...");

            RuleFor(x => x.userDtoCreateRequest.Age)
                .NotEmpty().WithMessage("Age de l'utilisateur : Requis ...");

            RuleFor(x => x.userDtoCreateRequest.Email)
                .NotEmpty().WithMessage("Email de l'utilisateur : Requis ...")
                .EmailAddress().EmailAddress().WithMessage("Email de l'utilisateur : Format invalide ...")
                .MaximumLength(50).WithMessage("Email de l'utilisateur : Caractères maximum (50) ...");

            RuleFor(x => x.userDtoCreateRequest.PasswordHashed)
                .NotEmpty().WithMessage("Mot de passe de l'utilisateur : Requis ...")
                .MinimumLength(10).WithMessage("Mot de passe de l'utilisateur : Caractères minimum (1O) ...")
                .MaximumLength(255).WithMessage("Mot de passe de l'utilisateur : Caractères maximum (255) ...");

            RuleFor(x => x.userDtoCreateRequest.Phone)
                .NotEmpty().WithMessage("Numéro de l'utilisateur : Requis ...")
                .MaximumLength(10).WithMessage("Numéro de l'utilisateur : Caractères maximum (10) ...");

            RuleFor(x => x.userDtoCreateRequest.ClubId)
                .NotEmpty().WithMessage("Identifiant du club : Requis ...")
                .MustAsync(async (clubId, cancellation) =>
                await db.Clubs.AsNoTracking().AnyAsync(c => c.ClubId == clubId, cancellation))
                .WithMessage(club => $"Aucun club trouvé avec l'identifiant {club.userDtoCreateRequest.ClubId} ...");

            RuleFor(x => x.userDtoCreateRequest.UserTypeId)
                .NotEmpty().WithMessage("Identifiant du type d'utilisateur : Requis ...")
                .MustAsync(async (userTypeId, cancellation) =>
                await db.UserTypes.AsNoTracking().AnyAsync(c => c.UserTypeId == userTypeId, cancellation))
                .WithMessage(club => $"Aucun type d'utilisateur trouvé avec l'identifiant {club.userDtoCreateRequest.UserTypeId} ...");
        }
    }
}