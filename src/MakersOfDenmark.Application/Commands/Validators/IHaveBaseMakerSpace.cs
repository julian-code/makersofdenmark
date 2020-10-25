using FluentValidation;
using System;

namespace MakersOfDenmark.Application.Commands.Validators
{
    public interface IHaveBaseMakerSpace
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
    }
    public class BaseMakerSpaceValidator : AbstractValidator<IHaveBaseMakerSpace>
    {
        public BaseMakerSpaceValidator()
        {
            RuleFor(x => x.LogoUrl)
                .Must(url => 
                    Uri.TryCreate(url, UriKind.Absolute, out Uri outUri)
                    && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps)
                ).WithMessage("Enter a valid URL");
            RuleFor(x => x.Name).NotEmpty().WithMessage("MakerSpace must have a name");
        }
    }
}
