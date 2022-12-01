using FluentValidation;

namespace ShelterBuddy.CodePuzzle.Api.Models.Validations
{
    public class AnimalModelValidator : AbstractValidator<AnimalModel>
    {
        public AnimalModelValidator()
        {
            RuleFor(model => model.Name)
                .NotNull()
                .WithMessage("'Name' must not be null")
                .NotEmpty()
                .WithMessage("'Name' must not be empty");

            RuleFor(model => model.Species)
                .NotNull()
                .WithMessage("'Species' must not be null")
                .NotEmpty()
                .WithMessage("'Species' must not be empty");

            RuleFor(model => new { model.DateOfBirth, model.AgeMonths, model.AgeYears, model.AgeWeeks })
                .Must(item => item.DateOfBirth is not null || item.AgeYears is not null || item.AgeMonths is not null || item.AgeWeeks is not null)
                .WithMessage("'DateOfBirth' or 'Age' must be provided");
        }
    }
}
