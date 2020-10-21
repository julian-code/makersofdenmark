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
    public class EditMakerSpaceContactInfo : IRequest
    {
        public Guid MakerSpaceId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
    public class EditMakerSpaceContactInfoHandler : IRequestHandler<EditMakerSpaceContactInfo>
    {
        private readonly MODContext _context;

        public EditMakerSpaceContactInfoHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(EditMakerSpaceContactInfo request, CancellationToken cancellationToken = default)
        {
            var makerSpace = await _context.MakerSpace.Include(x => x.ContactInfo).FirstOrDefaultAsync(x => x.Id == request.MakerSpaceId);
            if (makerSpace == null)
            {
                throw new NullReferenceException("Cannot find MakerSpace");
            }
            var newContactInfo = new ContactInfo { Email = request.Email, Phone = request.Phone };
            makerSpace.ContactInfo = newContactInfo;
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}
