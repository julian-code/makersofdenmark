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

namespace MakersOfDenmark.Application.Commands.V2.Makerspace
{
    public class UpdateMakerSpaceTool : IRequest
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }
    public class UpdateMakerSpaceToolHandler : IRequestHandler<UpdateMakerSpaceTool>
    {
        private readonly MODContext _context;

        public UpdateMakerSpaceToolHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateMakerSpaceTool request, CancellationToken cancellationToken)
        {
            var tool = await _context.Set<Tool>().FirstOrDefaultAsync(x => x.Id == request.Id);
            tool.UpdateTool(request);
            _context.Entry(tool).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
    public static class ToolExtension
    {
        public static Tool UpdateTool(this Tool tool, UpdateMakerSpaceTool request)
        {
            if (!string.IsNullOrWhiteSpace(request.Make)) { tool.Make = request.Make; }
            if (!string.IsNullOrWhiteSpace(request.Model)) { tool.Model = request.Model; }
            
            return tool;
        }
    }
}
