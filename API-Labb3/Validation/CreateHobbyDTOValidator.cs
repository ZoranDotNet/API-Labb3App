using API_Labb3.DTOs;
using FluentValidation;

namespace API_Labb3.Validation
{
    public class CreateHobbyDTOValidator : AbstractValidator<CreateHobbyDTO>
    {
        public CreateHobbyDTOValidator()
        {
            RuleFor(h => h.Title).NotEmpty().WithMessage(ValidationsUtilities.NonEmptyMessage)
                .MaximumLength(100).WithMessage(ValidationsUtilities.MaximumLengthMessage)
                .Must(ValidationsUtilities.FirstLetterIsUpperCase)
                .WithMessage(ValidationsUtilities.FirstLetterIsUpperCaseMessage);

            RuleFor(h => h.Description).NotEmpty().WithMessage(ValidationsUtilities.NonEmptyMessage)
                .MaximumLength(300).WithMessage(ValidationsUtilities.MaximumLengthMessage)
                .Must(ValidationsUtilities.FirstLetterIsUpperCase).WithMessage(ValidationsUtilities.FirstLetterIsUpperCaseMessage);
        }
    }
}
