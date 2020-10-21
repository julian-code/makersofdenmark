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
    public class EditMakerSpaceOrganization : IRequest
    {
        public Guid Id { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationStreet { get; set; }
        public string OrganizationCity { get; set; }
        public string OrganizationPostCode { get; set; }
        public string OrganizationCountry { get; set; }
        public OrganizationType OrganizationType { get; set; }
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
            var makerSpace = await _context.MakerSpace.Include(x => x.Organization).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (makerSpace == null)
            {
                throw new NullReferenceException("Cannot find MakerSpace");
            }
            var newOrgAddress = new Address(request.OrganizationStreet, request.OrganizationCity, request.OrganizationCountry, request.OrganizationPostCode);
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
