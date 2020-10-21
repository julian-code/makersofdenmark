using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Commands.V1.admin
{
    public class RemoveMakerSpaceTool : IRequest
    {
        public Guid MakerSpaceId { get; set; }
        public int ToolId { get; set; }
    }
    public class RemoveMakerSpaceToolsHandler : IRequestHandler<RemoveMakerSpaceTool>
    {
        private readonly MODContext _context;

        public RemoveMakerSpaceToolsHandler(MODContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveMakerSpaceTool request, CancellationToken cancellationToken = default)
        {
            var makerSpace = await _context.MakerSpace.Include(x => x.Tools).FirstOrDefaultAsync(x => x.Id == request.MakerSpaceId);
            if (makerSpace == default)
            {
                throw new NullReferenceException("Cannot find MakerSpace");
            }
            var tool = makerSpace.Tools.FirstOrDefault(x => x.Id == request.ToolId);
          
            if (tool == default)
            {
                throw new Exception("Tool not found on MakerSpace");
            }
            makerSpace.Tools.Remove(tool);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}
