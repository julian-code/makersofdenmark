using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Commands.V2.Makerspace
{
    public class DeleteMakerSpace : IRequest
    {
        public Guid Id { get; set; }
        public DeleteMakerSpace(Guid id)
        {
            Id = id;
        }
    }
    public class DeleteMakerSpaceHandler : IRequestHandler<DeleteMakerSpace>
    {
        private readonly MODContext _context;

        public DeleteMakerSpaceHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteMakerSpace request, CancellationToken cancellationToken)
        {
            var makerspace = await _context.MakerSpace.FirstOrDefaultAsync(x => x.Id == request.Id);
            _context.MakerSpace.Remove(makerspace);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}
