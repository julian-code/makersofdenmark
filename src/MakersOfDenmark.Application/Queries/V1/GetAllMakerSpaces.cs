using MakersOfDenmark.Domain.Enums;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Queries.V1
{
    public class GetAllMakerSpaces : IRequest<List<MakerSpaceResponse>>
    {
    }

    public class GetAllMakerSpacesRequestHandler : IRequestHandler<GetAllMakerSpaces, List<MakerSpaceResponse>>
    {
        private readonly MODContext _context;

        public GetAllMakerSpacesRequestHandler(MODContext context)
        {
            _context = context;
        }

        public async Task<List<MakerSpaceResponse>> Handle(GetAllMakerSpaces request, CancellationToken cancellationToken = default)
        {
            var makerspaces = await _context.MakerSpace
                .Include(x=>x.Address)
                .Include(x=>x.ContactInfo)
                .Include(x=>x.Organization).ThenInclude(o=>o.Address)
                .Include(x=>x.Tools)
                .AsNoTracking().ToListAsync();
            var response = new List<MakerSpaceResponse>();
            makerspaces.ForEach(x => response.Add(MakerSpaceResponse.Create(x)));
            return response;
        }
    }
    public class MakerSpaceResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AddressViewmodel Address { get; set; }
        public ContactInformationViewModel ContactInformation { get; set; }
        public string VATNumber { get; set; }
        public string LogoUrl { get; set; }
        public AccessType AccessType { get; set; }
        public OrganizationViewmodel Organization { get; set; }
        public List<string> Tools { get; set; } = new List<string>();

        internal static MakerSpaceResponse Create(MakerSpace ms)
        {
            var msResponse = new MakerSpaceResponse();
            msResponse.Id = ms.Id;
            msResponse.Name = ms.Name;
            if (!(ms.Address is null))
            {
                msResponse.Address = AddressViewmodel.Create(ms.Address);
            }
            if (!(ms.ContactInfo is null))
            {
                msResponse.ContactInformation = ContactInformationViewModel.Create(ms.ContactInfo);
            }
            msResponse.VATNumber = ms.VATNumber;
            msResponse.LogoUrl = ms.Logo.ToString();
            ms.AccessType = ms.AccessType;
            if (!(ms.Organization is null))
            {
                msResponse.Organization = OrganizationViewmodel.Create(ms.Organization);
            }
            ms.Tools.ToList().ForEach(x=> msResponse.Tools.Add($"{x.Make} {x.Model}"));

            return msResponse;
        }

        public class OrganizationViewmodel
        {
            public string Name { get; set; }
            public AddressViewmodel Address { get; set; }

            internal static OrganizationViewmodel Create(Organization organization)
            {
                var orgVM = new OrganizationViewmodel();
                orgVM.Name = organization.Name;
                orgVM.Address = AddressViewmodel.Create(organization.Address);
                return orgVM;
            }
        }
        public class AddressViewmodel
        {
            public string Street { get; set; }
            public string PostCode { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            internal static AddressViewmodel Create(Address address)
            {
                var addressVM = new AddressViewmodel();
                addressVM.Street = address.Street;
                addressVM.PostCode = address.PostCode;
                addressVM.City = address.City;
                addressVM.Country = address.Country;
                return addressVM;
            }
        }
        public class ContactInformationViewModel
        {
            public string Email { get; set; }
            public string Phone { get; set; }
            internal static ContactInformationViewModel Create(ContactInfo contactInfo)
            {
                var ciVM = new ContactInformationViewModel();
                ciVM.Email = contactInfo.Email;
                ciVM.Phone = contactInfo.Phone;
                return ciVM;
            }
        }
    }
}
