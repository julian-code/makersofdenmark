using MakersOfDenmark.Domain.Enums;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Queries.V1
{
    public class GetSelectionOfMakerSpaces : IRequest<GetSelectionOfMakerSpacesResponse>
    {
        public GetSelectionOfMakerSpaces(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
    
    public class GetSelectionOfMakerSpacesHandler : IRequestHandler<GetSelectionOfMakerSpaces, GetSelectionOfMakerSpacesResponse>
    {
        private readonly MODContext _context;

        public GetSelectionOfMakerSpacesHandler(MODContext context)
        {
            _context = context;
        }

        public async Task<GetSelectionOfMakerSpacesResponse> Handle(GetSelectionOfMakerSpaces request, CancellationToken cancellationToken = default)
        {
            var makerSpace = await _context.MakerSpace.Include(x => x.Address).AsNoTracking().FirstAsync(x => x.Name == request.Name);
            return new GetSelectionOfMakerSpacesResponse(makerSpace);
        }
    }

    public class GetSelectionOfMakerSpacesResponse
    {
        public GetSelectionOfMakerSpacesResponse(MakerSpace makerSpace)
        {
            Id = makerSpace.Id.ToString();
            Name = makerSpace.Name;
            Address = makerSpace.Address.FullAddress;
            Logo = makerSpace.Logo.ToString();
            AccessType = makerSpace.AccessType.ToString();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public string AccessType { get; set; }
    }
}
