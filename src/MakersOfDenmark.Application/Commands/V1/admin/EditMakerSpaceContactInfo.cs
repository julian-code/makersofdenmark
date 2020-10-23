using FluentValidation;
using MakersOfDenmark.Application.Commands.Validators;
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
    public class EditMakerSpaceContactInfo : IMakerSpaceIdentifier, IContactInfo, IRequest
    {
        public Guid MakerSpaceId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
    public class EditMakerSpaceContactInfoValidator : AbstractValidator<EditMakerSpaceContactInfo>
    {
        private readonly MODContext _context;

        public EditMakerSpaceContactInfoValidator(MODContext context)
        {
            _context = context;
            Include(new MakerSpaceIdentifierValidator(_context));
            Include(new ContactInfoValidator());
        }
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
            var newContactInfo = new ContactInfo { Email = request.Email, Phone = request.Phone };
            makerSpace.ContactInfo = newContactInfo;
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}
