using FluentValidation;
using MakersOfDenmark.Application.Commands.Validators;
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
    public class RegisterMakerSpace : IHaveBaseMakerSpace, IHaveAddress, IHaveContactInfo, IRequest<Guid>
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; } = "Denmark";
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string VATNumber { get; set; }
        public string LogoUrl { get; set; }
        public AccessType AccessType { get; set; } = 0;
    }
    public class RegisterMakerSpaceValidator : AbstractValidator<RegisterMakerSpace>
    {
        public RegisterMakerSpaceValidator()
        {
            Include(new BaseMakerSpaceValidator());
            Include(new AddressValidator());
            Include(new ContactInfoValidator());
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
            var newAddress = new Address(request.Street, request.City, request.Country, request.PostCode);
            var newContactInfo = new ContactInfo { Email = request.Email, Phone = request.Phone };
            var newMakerSpace = new MakerSpace
            {
                Name = request.Name,
                Address = newAddress,
                ContactInfo = newContactInfo,
                VATNumber = request.VATNumber,
                Logo = request.LogoUrl,
                AccessType = request.AccessType
            };
            _context.MakerSpace.Add(newMakerSpace);
            await _context.SaveChangesAsync();
            return newMakerSpace.Id;
        }
    }
}
