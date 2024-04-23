using API_Labb3.DTOs;
using FluentValidation;

namespace API_Labb3.Validation
{
    public class CreatePersonDTOValidator : AbstractValidator<CreatePersonDTO>
    {
        public CreatePersonDTOValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage(ValidationsUtilities.NonEmptyMessage)
                .MaximumLength(100).WithMessage(ValidationsUtilities.MaximumLengthMessage)
                .Must(ValidationsUtilities.FirstLetterIsUpperCase).WithMessage(ValidationsUtilities.FirstLetterIsUpperCaseMessage);

            RuleFor(p => p.Email).NotEmpty().WithMessage(ValidationsUtilities.NonEmptyMessage)
                .EmailAddress().WithMessage(ValidationsUtilities.EmailMessage);

            RuleFor(p => p.PhoneNumber).MaximumLength(20).WithMessage(ValidationsUtilities.MaximumLengthMessage);
        }
    }
}
