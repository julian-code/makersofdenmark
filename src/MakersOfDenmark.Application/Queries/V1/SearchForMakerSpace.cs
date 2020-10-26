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
    public class SearchForMakerSpace : IRequest<List<SearchForMakerSpaceResponse>>
    {
        public SearchForMakerSpace(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public class SearchForMakerSpaceHandler : IRequestHandler<SearchForMakerSpace, List<SearchForMakerSpaceResponse>>
    {
        private readonly MODContext _context;

        public SearchForMakerSpaceHandler(MODContext context)
        {
            _context = context;
        }

        public async Task<List<SearchForMakerSpaceResponse>> Handle(SearchForMakerSpace request, CancellationToken cancellationToken = default)
        {
            var makerSpaces = await _context.MakerSpace.Include(x => x.Address).AsNoTracking().Where(x => x.Name.Contains(request.Name)).ToListAsync();
           
            var response = new List<SearchForMakerSpaceResponse>();
            makerSpaces.ForEach(x => response.Add(new SearchForMakerSpaceResponse(x)));
            return response;
        } 
    }
    public class SearchForMakerSpaceResponse
    {
        public SearchForMakerSpaceResponse(MakerSpace makerSpace)
        {
            Id = makerSpace.Id;
            Name = makerSpace.Name;
            Address = makerSpace.Address.FullAddress;
            Logo = makerSpace.Logo.ToString();
            AccessType = makerSpace.AccessType;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public AccessType AccessType { get; set; }
        
    }
}