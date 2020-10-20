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
    public class GetMakerSpaceToolsById : IRequest<GetMakerSpaceByToolsByIdResponse>
    {
        public GetMakerSpaceToolsById(Guid makerSpaceId)
        {
            MakerSpaceId = makerSpaceId;
        }

        public Guid MakerSpaceId { get; }
    }

    public class GetMakerSpaceToolsByIdHandler : IRequestHandler<GetMakerSpaceToolsById, GetMakerSpaceByToolsByIdResponse>
    {
        private readonly MODContext _context;

        public GetMakerSpaceToolsByIdHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<GetMakerSpaceByToolsByIdResponse> Handle(GetMakerSpaceToolsById request, CancellationToken cancellationToken = default)
        {
            var makerSpaceTools = await _context.MakerSpace
                .Include(x => x.Tools)
                    .ThenInclude(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == request.MakerSpaceId);

            if (makerSpaceTools is null)
            {
                return null;
            }

            return new GetMakerSpaceByToolsByIdResponse(makerSpaceTools.Tools);
            
        }
    }

    public class GetMakerSpaceByToolsByIdResponse
    {
        public IEnumerable<ToolViewModel> Tools { get; set; }

        public GetMakerSpaceByToolsByIdResponse(IEnumerable<Tool> tools)
        {
            Tools = tools.Select(x => new ToolViewModel(x));
        }
    }
}
