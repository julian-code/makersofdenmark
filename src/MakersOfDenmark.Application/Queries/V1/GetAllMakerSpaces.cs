using MakersOfDenmark.Domain.Enums;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Queries.V1
{
    public class GetAllMakerSpaces : IRequest<List<MakerSpaceViewmodel>>
    {
    }

    public class GetAllMakerSpacesRequestHandler : IRequestHandler<GetAllMakerSpaces, List<MakerSpaceViewmodel>>
    {
        private readonly MODContext _context;

        public GetAllMakerSpacesRequestHandler(MODContext context)
        {
            _context = context;
        }

        public async Task<List<MakerSpaceViewmodel>> Handle(GetAllMakerSpaces request, CancellationToken cancellationToken = default)
        {
            var makerspaces = await _context.MakerSpace
                .Include(x=>x.Address)
                .Include(x=>x.ContactInfo)
                .Include(x=>x.Tools)
                .AsNoTracking().ToListAsync();
            var viewmodels =  makerspaces.Select(MakerSpaceViewmodel.Create).ToList();
            return viewmodels;
        }
    }
    public class GetAllMakerSpacesResponse
    {
        public List<MakerSpaceViewmodel> MakerSpaces { get; set; }
    }


    public class MakerSpaceViewmodel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string WorkShopType { get; set; }
        public string Description { get; set; }
        public AddressViewmodel Address { get; set; }
        public ContactInformationViewModel ContactInformation { get; set; }
        public string VATNumber { get; set; }
        public string LogoUrl { get; set; }
        public AccessType AccessType { get; set; }
        public string Organization { get; set; }
        public List<string> Tools { get; set; } = new List<string>();

        internal static MakerSpaceViewmodel Create(MakerSpace ms)
        {
            var msResponse = new MakerSpaceViewmodel();
            msResponse.Id = ms.Id;
            msResponse.Name = ms.Name;
            msResponse.WorkShopType = ms.WorkShopType;
            msResponse.Description = ms.Description;
            if (!(ms.Address is null))
            {
                msResponse.Address = AddressViewmodel.Create(ms.Address);
            }
            if (!(ms.ContactInfo is null))
            {
                msResponse.ContactInformation = ContactInformationViewModel.Create(ms.ContactInfo);
            }
            msResponse.VATNumber = ms.VATNumber;
            msResponse.LogoUrl = ms.Logo;
            ms.AccessType = ms.AccessType;
            if (!string.IsNullOrWhiteSpace(ms.Organization))
            {
                msResponse.Organization = ms.Organization;
            }
            msResponse.Tools = ms.Tools.Select(x=>x.Name).ToList();

            return msResponse;
        }

        public class AddressViewmodel
        {
            public string Street { get; set; }
            public string PostCode { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public static AddressViewmodel Create(Address address)
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
            public static ContactInformationViewModel Create(ContactInfo contactInfo)
            {
                var ciVM = new ContactInformationViewModel();
                ciVM.Email = contactInfo.Email;
                ciVM.Phone = contactInfo.Phone;
                return ciVM;
            }
        }
    }
}
