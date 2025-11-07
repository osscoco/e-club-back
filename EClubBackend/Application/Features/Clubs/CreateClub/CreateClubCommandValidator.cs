using FluentValidation;

namespace E_Club.Application.Features.Clubs.CreateClub
{
    public class CreateClubCommandValidator : AbstractValidator<CreateClubCommand>
    {
        public CreateClubCommandValidator() 
        {
            RuleFor(x => x.clubDtoCreateRequest.Name)
                .NotEmpty().WithMessage("Nom du club : Requis ...")
                .MaximumLength(50).WithMessage("Nom du club : Caractères maximum (50) ...");
            
            RuleFor(x => x.clubDtoCreateRequest.CA)
                .NotEmpty().WithMessage("Chiffre d'Affaire : Requis ...")
                .PrecisionScale(14,2,true).WithMessage("Chiffre d'Affaire : Précision (14 avant la virgule, 2 après la virgule) ...");
        }
    }
}