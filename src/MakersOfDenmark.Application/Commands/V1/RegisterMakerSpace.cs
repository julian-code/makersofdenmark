using FluentValidation;
using MakersOfDenmark.Domain.Enums;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Commands.V1
{
    public class RegisterMakerSpace : IRequest<Guid>
    {
        public string Name { get; set; }
        public string AddressStreet { get; set; }
        public string AddressPostCode { get; set; }
        public string AddressCountry { get; set; }
        public string AddressCity { get; set; }
        public string ContactInfoPhone { get; set; }
        public string ContactInfoEmail { get; set; }
        public string VATNumber { get; set; }
        public string LogoUrl { get; set; }
        public AccessType AccessType { get; set; }
    }
    public class RegisterMakerSpaceValidator : AbstractValidator<RegisterMakerSpace>
    {
        public RegisterMakerSpaceValidator()
        {
            RuleFor(x => x.LogoUrl)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out Uri outUri)
                && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps)
            ).WithMessage("Enter a valid URL");
            RuleFor(x => x.Name).NotEmpty().WithMessage("MakerSpace must have a name");
            RuleFor(x => x.AddressStreet).NotEmpty().WithMessage("MakerSpace must have street address");
            RuleFor(x => x.AddressCity).NotEmpty().WithMessage("MakerSpace must have city");
            RuleFor(x => x.AddressPostCode).NotEmpty().WithMessage("MakerSpace must have post code");
            RuleFor(x => x.AddressCountry).NotEmpty().WithMessage("MakerSpace must have country");
            RuleFor(x => x.ContactInfoPhone).NotEmpty().WithMessage("MakerSpace Must have a contact phone number");
            RuleFor(x => x.ContactInfoEmail).NotEmpty().WithMessage("MakerSpace Must have a contact email");
        }
    }

    public class RegisterMakerSpaceHandler : IRequestHandler<RegisterMakerSpace, Guid>
    {
        private readonly MODContext _context;

        public RegisterMakerSpaceHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(RegisterMakerSpace request, CancellationToken cancellationToken = default)
        {
            var newAddress = new Address(request.AddressStreet, request.AddressCity, request.AddressCountry, request.AddressPostCode);
            var newContactInfo = new ContactInfo { Email = request.ContactInfoEmail, Phone = request.ContactInfoPhone };
            var newMakerSpace = new MakerSpace
            {
                Name = request.Name,
                Address = newAddress,
                ContactInfo = newContactInfo,
                VATNumber = request.VATNumber,
                Logo = new Uri(request.LogoUrl),
                AccessType = request.AccessType
            };
            _context.MakerSpace.Add(newMakerSpace);
            await _context.SaveChangesAsync();
            return newMakerSpace.Id;
        }
    }
}
