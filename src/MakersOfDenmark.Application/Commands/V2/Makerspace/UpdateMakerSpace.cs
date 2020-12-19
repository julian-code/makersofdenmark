using MakersOfDenmark.Domain.Enums;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Commands.V2.Makerspace
{
    public class UpdateMakerSpace : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string WorkShopType { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string AssociatedSchool { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public AccessType AccessType { get; set; }
        public string Organization { get; set; }
        public string VATNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
    public class UpdateMakerSpaceHandler : IRequestHandler<UpdateMakerSpace>
    {
        private readonly MODContext _context;

        public UpdateMakerSpaceHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateMakerSpace request, CancellationToken cancellationToken)
        {
            var makerspace = await _context.MakerSpace.Include(x => x.Address).Include(x => x.ContactInfo).FirstOrDefaultAsync(x => x.Id == request.Id);
            makerspace.UpdateMakerSpace(request);
            _context.Entry(makerspace).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
    public static class MakerSpaceExtension
    {
        public static MakerSpace UpdateMakerSpace(this MakerSpace makerSpace, UpdateMakerSpace request) 
        {
            if(!string.IsNullOrWhiteSpace(request.Name)) { makerSpace.Name = request.Name; }
            if (!string.IsNullOrWhiteSpace(request.WorkShopType)) { makerSpace.WorkShopType = request.WorkShopType; }
            if (!string.IsNullOrWhiteSpace(request.AssociatedSchool)) { makerSpace.AssociatedSchool = request.AssociatedSchool; }
            if (!string.IsNullOrWhiteSpace(request.Description)) { makerSpace.Description = request.Description; }
            if (!(request.Logo is null)) { makerSpace.Logo = request.Logo; }
            if (!(request.AccessType== AccessType.None)) { makerSpace.AccessType = request.AccessType; }
            if (!string.IsNullOrWhiteSpace(request.Organization)) { makerSpace.Organization = request.Organization; }
            if (!string.IsNullOrWhiteSpace(request.VATNumber)) { makerSpace.VATNumber = request.VATNumber; }
            if (!string.IsNullOrWhiteSpace(request.Phone) && !string.IsNullOrWhiteSpace(request.Email)) {
                makerSpace.ContactInfo = new ContactInfo { Phone = request.Phone, Email = request.Email };
                }
            makerSpace.Address = CreateNewValidAddress(makerSpace.Address, request);
            
            return makerSpace;
        }
        private static Address CreateNewValidAddress(Address current, UpdateMakerSpace request)
        {
            if (string.IsNullOrWhiteSpace(request.Street)) { request.Street = current.Street; }
            if (string.IsNullOrWhiteSpace(request.City)) { request.City = current.City; }
            if (string.IsNullOrWhiteSpace(request.PostCode)) { request.PostCode = current.PostCode; }
            if (string.IsNullOrWhiteSpace(request.Country)) { request.Country = current.Country; }
            return new Address(request.Street, request.Country, request.Country, request.PostCode);
        }
    }
}
