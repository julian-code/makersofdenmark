using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Queries.V1
{
    public class GetMakerSpaceById : IRequest<GetMakerSpaceByIdResponse>
    {
        public GetMakerSpaceById(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    public class GetMakerSpaceByIdHandler : IRequestHandler<GetMakerSpaceById, GetMakerSpaceByIdResponse>
    {
        private readonly MODContext _context;

        public GetMakerSpaceByIdHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<GetMakerSpaceByIdResponse> Handle(GetMakerSpaceById request, CancellationToken cancellationToken = default)
        {
            var makerSpace = await _context.MakerSpace.AsNoTracking()
                .Include(x => x.Tools)
                    .ThenInclude(x => x.Categories)
                .Include(x => x.Address)
                .Include(x => x.Organization)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return new GetMakerSpaceByIdResponse(makerSpace);
        }
    }

    public class GetMakerSpaceByIdResponse
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Organization { get; set; }
        public IEnumerable<ToolViewModel> Tools { get; set; }

        public GetMakerSpaceByIdResponse(MakerSpace makerSpace)
        {
            Id = makerSpace.Id;
            Address = makerSpace.Address.FullAddress;
            Organization = makerSpace.Organization.Name;
            Tools = makerSpace.Tools.Select(x => new ToolViewModel(x));
        }
    }

    public class ToolViewModel
    {
        public string Name { get; set; }
        public string[] Categories { get; set; }

        public ToolViewModel(Tool tool)
        {
            Categories = tool.Categories.Select(x => x.Title).ToArray();
            Name = tool.Name;
        }
    }
}
