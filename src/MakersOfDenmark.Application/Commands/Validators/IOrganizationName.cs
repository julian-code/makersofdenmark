using FluentValidation;

namespace MakersOfDenmark.Application.Commands.Validators
{
    public interface IOrganizationName
    {
        public string OrganizationName { get; set; }
    }
    public class OrganizationNameValidator : AbstractValidator<IOrganizationName>
    {
        public OrganizationNameValidator()
        {
            RuleFor(x => x.OrganizationName).NotEmpty().WithMessage("Organization Name must be provided");
        }
    }
}
