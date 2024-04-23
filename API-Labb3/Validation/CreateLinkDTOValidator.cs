using API_Labb3.DTOs;
using FluentValidation;

namespace API_Labb3.Validation
{
    public class CreateLinkDTOValidator : AbstractValidator<CreateLinkDTO>
    {
        public CreateLinkDTOValidator()
        {
            RuleFor(l => l.Url).NotEmpty().WithMessage(ValidationsUtilities.NonEmptyMessage);
        }
    }
}
