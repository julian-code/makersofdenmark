using FluentValidation;
using MakersOfDenmark.Domain.Enums;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Commands.V1.admin
{
    public class EditMakerSpaceAddress : IRequest
    {
        public Guid MakerSpaceId { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
    public class EditMakerSpaceAddressValidator : AbstractValidator<EditMakerSpaceAddress>
    {
        public EditMakerSpaceAddressValidator()
        {
            RuleFor(x => x.Street).NotEmpty().WithMessage("MakerSpace must have street address");
            RuleFor(x => x.City).NotEmpty().WithMessage("MakerSpace must have city");
            RuleFor(x => x.PostCode).NotEmpty().WithMessage("MakerSpace must have post code");
            RuleFor(x => x.Country).NotEmpty().WithMessage("MakerSpace must have country");
        }
    }

    public class EditMakerSpaceAddressHandler : IRequestHandler<EditMakerSpaceAddress>
    {
        private readonly MODContext _context;

        public EditMakerSpaceAddressHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(EditMakerSpaceAddress request, CancellationToken cancellationToken = default)
        {
            var makerSpace = await _context.MakerSpace.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == request.MakerSpaceId);
            if (makerSpace == null)
            {
                throw new NullReferenceException("Cannot find MakerSpace");
            }
            var newAddress = new Address(request.Street, request.City, request.Country, request.Country);
            makerSpace.Address = newAddress;
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}
