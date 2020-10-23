using FluentValidation;

namespace MakersOfDenmark.Application.Commands.Validators
{
    public interface IAddress
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }

    }
    public class AddressValidator : AbstractValidator<IAddress>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Street).NotEmpty().WithMessage("must have street address");
            RuleFor(x => x.City).NotEmpty().WithMessage("must have city");
            RuleFor(x => x.PostCode).NotEmpty().WithMessage("must have post code");
            RuleFor(x => x.Country).NotEmpty().WithMessage("must have country");
        }
    }
}
