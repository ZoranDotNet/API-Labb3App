using API_Labb3.DTOs;
using FluentValidation;

namespace API_Labb3.Validation
{
    public class CreateHobbyDTOValidator : AbstractValidator<CreateHobbyDTO>
    {
        public CreateHobbyDTOValidator()
        {
            RuleFor(h => h.Title).Must(ValidationsUtilities.FirstLetterIsUpperCase)
                .WithMessage(ValidationsUtilities.FirstLetterIsUpperCaseMessage)
                .MaximumLength(100).WithMessage(ValidationsUtilities.MaximumLengthMessage);

            RuleFor(h => h.Description).MaximumLength(300).WithMessage(ValidationsUtilities.MaximumLengthMessage)
                .Must(ValidationsUtilities.FirstLetterIsUpperCase).WithMessage(ValidationsUtilities.FirstLetterIsUpperCaseMessage);
        }
    }
}
