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
        }
    }
}
