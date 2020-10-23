using FluentValidation;
using MakersOfDenmark.Application.Commands.Validators;
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

namespace MakersOfDenmark.Application.Commands.V1.admin
{
    public class EditMakerSpaceOrganization : IMakerSpaceIdentifier, IOrganizationName, IAddress, IRequest
    {
        public Guid MakerSpaceId { get; set; }
        public string OrganizationName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public OrganizationType OrganizationType { get; set; } = 0;
    }
    public class EditMakerSpaceOrganizationValidator : AbstractValidator<EditMakerSpaceOrganization>
    {
        private readonly MODContext _context;

        public EditMakerSpaceOrganizationValidator(MODContext context)
        {
            _context = context;
            Include(new MakerSpaceIdentifierValidator(_context));
            Include(new OrganizationNameValidator());
            Include(new AddressValidator());
        }
    }
    public class EditMakerSpaceOrganizationHandler : IRequestHandler<EditMakerSpaceOrganization>
    {
        private readonly MODContext _context;

        public EditMakerSpaceOrganizationHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(EditMakerSpaceOrganization request, CancellationToken cancellationToken = default) 
        {
            var makerSpace = await _context.MakerSpace.Include(x => x.Organization).FirstOrDefaultAsync(x => x.Id == request.MakerSpaceId);
            var newOrgAddress = new Address(request.Street, request.City, request.Country, request.PostCode);
            var newOrganization = new Organization
            {
                Address = newOrgAddress,
                Name = request.OrganizationName,
                OrganizationType = request.OrganizationType
            };
            makerSpace.Organization = newOrganization;
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}
