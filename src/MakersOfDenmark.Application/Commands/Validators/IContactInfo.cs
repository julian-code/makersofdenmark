using FluentValidation;

namespace MakersOfDenmark.Application.Commands.Validators
{
    public interface IContactInfo
    {
        public string Phone { get; set; }
        public string Email { get; set; }
    }
    public class ContactInfoValidator : AbstractValidator<IContactInfo>
    {
        public ContactInfoValidator()
        {
            RuleFor(x => x.Phone).NotEmpty().WithMessage("MakerSpace Must have a contact phone number");
            RuleFor(x => x.Email).NotEmpty().WithMessage("MakerSpace Must have a contact email");
        }
    }
}
