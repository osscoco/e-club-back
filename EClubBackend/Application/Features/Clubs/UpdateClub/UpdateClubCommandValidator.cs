using FluentValidation;

namespace E_Club.Application.Features.Clubs.UpdateClub
{
    public class UpdateClubCommandValidator : AbstractValidator<UpdateClubCommand>
    {
        public UpdateClubCommandValidator()
        {
            RuleFor(x => x.clubDtoUpdateRequest.Name)
                .NotEmpty().WithMessage("Nom du club : Requis ...")
                .MaximumLength(50).WithMessage("Nom du club : Caractères maximum (50) ...");

            RuleFor(x => x.clubDtoUpdateRequest.CA)
                .NotEmpty().WithMessage("Chiffre d'Affaire du club : Requis ...")
                .PrecisionScale(14, 2, true).WithMessage("Chiffre d'Affaire du club : Précision (14 avant la virgule, 2 après la virgule) ...");
        }
    }
}
