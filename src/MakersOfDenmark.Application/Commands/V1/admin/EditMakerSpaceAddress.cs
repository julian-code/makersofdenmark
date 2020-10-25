using FluentValidation;
using MakersOfDenmark.Application.Commands.Validators;
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
    public class EditMakerSpaceAddress : IHaveAddress, IHaveMakerSpaceIdentifier, IRequest
    {
        public Guid MakerSpaceId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
    }
    public class EditMakerSpaceAddressValidator : AbstractValidator<EditMakerSpaceAddress>
    {
        public EditMakerSpaceAddressValidator(MODContext context)
        {
            Include(new MakerSpaceIdentifierValidator(context));
            Include(new AddressValidator());
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
            var newAddress = new Address(request.Street, request.City, request.Country, request.Country);
            makerSpace.Address = newAddress;
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}
