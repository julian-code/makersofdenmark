using MakersOfDenmark.Domain.Enums;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Commands.V2.Makerspace
{
    public class RegisterMakerSpaceV2 : IRequest<RegisterMakerSpaceResponseV2>
    {
        public string Name { get; set; }
        public string WorkShopType { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; } = "Denmark";
        public string AssociatedSchool { get; set; }
        public string Description { get; set; }
        public Uri Logo { get; set; }
        public AccessType AccessType { get; set; }
        public string Organization { get; set; }
        public string VATNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
    public class RegisterMakerSpaceV2Handler : IRequestHandler<RegisterMakerSpaceV2, RegisterMakerSpaceResponseV2>
    {
        private readonly MODContext _context;

        public RegisterMakerSpaceV2Handler(MODContext context)
        {
            _context = context;
        }
        public async Task<RegisterMakerSpaceResponseV2> Handle(RegisterMakerSpaceV2 request, CancellationToken cancellationToken)
        {
            var makerspace = Create(request);
            _context.MakerSpace.Add(makerspace);
            await _context.SaveChangesAsync();
            return new RegisterMakerSpaceResponseV2 { Id = makerspace.Id };
        }
        private MakerSpace Create(RegisterMakerSpaceV2 request)
        {
            var newAddress = new Address(request.Street, request.City, request.Country, request.PostCode);
            var newContactInfo = new ContactInfo { Email = request.Email, Phone = request.Phone };
            var ms = new MakerSpace();
            ms.Name = request.Name;
            ms.WorkShopType = request.WorkShopType;
            ms.AssociatedSchool = request.AssociatedSchool;
            ms.Description = request.Description;
            ms.AccessType = request.AccessType;
            ms.Organization = request.Organization;
            ms.VATNumber = request.VATNumber;
            ms.Address = newAddress;
            ms.ContactInfo = newContactInfo;
            return ms;
        }
    }
    public class RegisterMakerSpaceResponseV2
    {
        public Guid Id { get; set; }
    }
}
