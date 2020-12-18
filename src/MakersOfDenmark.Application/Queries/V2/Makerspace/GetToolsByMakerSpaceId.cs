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

namespace MakersOfDenmark.Application.Queries.V2.Makerspace
{
    public class GetToolsByMakerSpaceId : IRequest<List<GetToolsByMakerSpaceIdResponse>>
    {
        public Guid Id { get; set; }
        public GetToolsByMakerSpaceId(Guid id)
        {
            Id = id;
        }
    }
    public class GetToolsByMakerSpaceIdHandler : IRequestHandler<GetToolsByMakerSpaceId, List<GetToolsByMakerSpaceIdResponse>>
    {
        private readonly MODContext _context;

        public GetToolsByMakerSpaceIdHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<List<GetToolsByMakerSpaceIdResponse>> Handle(GetToolsByMakerSpaceId request, CancellationToken cancellationToken)
        {
            var makerspace = await _context.MakerSpace.Include(x => x.Tools).FirstOrDefaultAsync(x=> x.Id == request.Id);
            var response = new List<GetToolsByMakerSpaceIdResponse>();
            makerspace.Tools.ToList().ForEach(x=>response.Add(GetToolsByMakerSpaceIdResponse.Create(x)));
            return response;
        }
    }

    public class GetToolsByMakerSpaceIdResponse
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        public static GetToolsByMakerSpaceIdResponse Create(Tool tool)
            => new GetToolsByMakerSpaceIdResponse
            {
                Id = tool.Id,
                Make = tool.Make,
                Model = tool.Model
            };
    }
}
