using FluentValidation;
using Microsoft.EntityFrameworkCore;
using InfrastructureEFCore;

namespace E_Club.Application.Features.Auth.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator(AppDbContext db)
        {
            RuleFor(x => x.loginDtoRequest.Email)
                .NotEmpty().WithMessage("Email de l'utilisateur : Requis ...")
                .EmailAddress().WithMessage("Email de l'utilisateur : Format invalide ...")
                .MaximumLength(50).WithMessage("Email de l'utilisateur : Caractères maximum (50) ...")
                .MustAsync(async (userEmail, cancellation) =>
                await db.Users.AsNoTracking().AnyAsync(c => c.Email == userEmail, cancellation))
                .WithMessage(user => $"Aucun utilisateur trouvé avec l'email {user.loginDtoRequest.Email} ...");


            RuleFor(x => x.loginDtoRequest.PasswordHashed)
                .NotEmpty().WithMessage("Mot de passe de l'utilisateur : Requis ...")
                .MinimumLength(10).WithMessage("Mot de passe de l'utilisateur : Caractères minimum (1O) ...")
                .MaximumLength(255).WithMessage("Mot de passe de l'utilisateur : Caractères maximum (255) ...");
        }
    }
}