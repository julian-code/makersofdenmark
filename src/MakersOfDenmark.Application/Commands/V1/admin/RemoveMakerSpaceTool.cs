using FluentValidation;
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

namespace MakersOfDenmark.Application.Commands.V1.admin
{
    public class RemoveMakerSpaceTool : IRequest
    {
        public Guid MakerSpaceId { get; set; }
        public int ToolId { get; set; }
    }
    public class RemoveMakerSpaceToolValidator : AbstractValidator<RemoveMakerSpaceTool>
    {
        private readonly MODContext _context;
        private MakerSpace _makerSpace;
        public RemoveMakerSpaceToolValidator(MODContext context)
        {
            _context = context;
            
            RuleFor(x => x.MakerSpaceId).MustAsync(async (id, cancellation) =>
            {
                _makerSpace = await _context.MakerSpace.Include(x => x.Tools).FirstOrDefaultAsync(x => x.Id == id);
                return !(_makerSpace is null);
            }).WithMessage(x => $"MakerSpace not found by id {x.MakerSpaceId}")
            .DependentRules(() =>
            {
                RuleFor(x =>  x.ToolId ).Must(toolId =>
                {
                  var msTool = _makerSpace?.Tools.FirstOrDefault(x => x.Id == toolId);
                  return !(msTool is null);
                }).WithMessage(x => $"Tool does not exists on MakerSpace {x.MakerSpaceId}");
            });
        }
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
            var tool = makerSpace.Tools.FirstOrDefault(x => x.Id == request.ToolId);
            makerSpace.Tools.Remove(tool);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}
