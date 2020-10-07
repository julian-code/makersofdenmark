using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Queries.V1
{
    public class GetAllMakerSpaces : IRequest<List<MakerSpace>>
    {
    }

    public class GetAllMakerSpacesRequestHandler : IRequestHandler<GetAllMakerSpaces, List<MakerSpace>>
    {
        private readonly MODContext _context;

        public GetAllMakerSpacesRequestHandler(MODContext context)
        {
            _context = context;
        }

        public async Task<List<MakerSpace>> Handle(GetAllMakerSpaces request, CancellationToken cancellationToken = default)
        {


            return await _context.MakerSpace
                .Include(x=> x.Address)
                .Include(x => x.MakerSpaceType)
                .Include(x => x.Organization)
                .ToListAsync();
        }
    }
}
