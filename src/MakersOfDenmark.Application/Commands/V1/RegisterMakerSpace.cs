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
    public class RegisterMakerSpace : IRequest<RegisterMakerSpaceResponse>
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
        public string AccessType { get; set; }
    }

    public class RegisterMakerSpaceHandler : IRequestHandler<RegisterMakerSpace, RegisterMakerSpaceResponse>
    {
        private readonly MODContext _context;

        public RegisterMakerSpaceHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<RegisterMakerSpaceResponse> Handle(RegisterMakerSpace request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.AddressStreet, request.AddressCity, request.AddressCountry, request.AddressPostCode);
            var newContactInfo = new ContactInfo { Email = request.ContactInfoEmail, Phone = request.ContactInfoPhone };
            var newMakerSpace = new MakerSpace
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Address = newAddress,
                ContactInfo = newContactInfo,
                VATNumber = request.VATNumber,
                Logo = new Uri(request.LogoUrl),
                AccessType = (AccessType)Enum.Parse(typeof(AccessType), request.AccessType)
            };
            _context.MakerSpace.Add(newMakerSpace);
            await _context.SaveChangesAsync();
            return new RegisterMakerSpaceResponse { Id = newMakerSpace.Id };
        }
    }

    public class RegisterMakerSpaceResponse
    {
        public Guid Id { get; set; }
    }
}
