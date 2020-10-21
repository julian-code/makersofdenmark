using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Commands.V1.admin
{
    public class AddMakerSpaceTool : IRequest
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }
    public class AddMakerSpaceToolsHandler : IRequestHandler<AddMakerSpaceTool>
    {
        private readonly MODContext _context;

        public AddMakerSpaceToolsHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(AddMakerSpaceTool request, CancellationToken cancellationToken = default)
        {
            var tool = new Tool { Make = request.Make, Model = request.Model };
            var makerSpace = await _context.MakerSpace.Include(x => x.Tools).FirstOrDefaultAsync(x => x.Id == request.Id);
            var msTool = makerSpace.Tools.FirstOrDefault(x => x.Make == request.Make && x.Model == request.Model);
            if (msTool != default)
            {
                throw new Exception("Tool already exists on MakerSpace");
            }
            makerSpace.Tools.Add(tool);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}
