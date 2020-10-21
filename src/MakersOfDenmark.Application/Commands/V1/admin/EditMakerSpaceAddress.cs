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
        public Guid Id { get; set; }
        public string AddressStreet { get; set; }
        public string AddressPostCode { get; set; }
        public string AddressCountry { get; set; }
        public string AddressCity { get; set; }
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
            var makerSpace = await _context.MakerSpace.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (makerSpace == null)
            {
                throw new NullReferenceException("Cannot find MakerSpace");
            }
            var newAddress = new Address(request.AddressStreet, request.AddressCity, request.AddressCountry, request.AddressCountry);
            makerSpace.Address = newAddress;
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}
