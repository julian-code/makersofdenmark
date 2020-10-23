using FluentValidation;
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
    public class EditMakerSpaceContactInfoValidator : AbstractValidator<EditMakerSpaceContactInfo>
    {
        private readonly MODContext _context;

        public EditMakerSpaceContactInfoValidator(MODContext context)
        {
            _context = context;
            RuleFor(x => x.MakerSpaceId).MustAsync(async (id, cancellationToken) => {
                var makerSpace = await _context.MakerSpace.FirstOrDefaultAsync(x => x.Id == id);
                return makerSpace is null ? false : true;
            }).WithMessage("MakerSpace doesn't exist");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("MakerSpace Must have a contact phone number");
            RuleFor(x => x.Email).NotEmpty().WithMessage("MakerSpace Must have a contact email");
            _context = context;
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
