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
        }
    }
}
