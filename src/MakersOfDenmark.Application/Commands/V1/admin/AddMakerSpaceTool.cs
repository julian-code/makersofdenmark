using FluentValidation;
using FluentValidation.Results;
using MakersOfDenmark.Application.Commands.Validators;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Commands.V1.admin
{
    public class AddMakerSpaceTool : IRequest
    {
        public Guid MakerSpaceId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }

    public class AddMakerSpaceToolValidator : AbstractValidator<AddMakerSpaceTool>
    {
        private readonly MODContext _context;
        public AddMakerSpaceToolValidator(MODContext context)
        {
            _context = context;

            RuleFor(x => x.MakerSpaceId).SetValidator(new ExistsInDatabase<MakerSpace, Guid>(_context, "Tools"))
                .DependentRules(() =>
                {
                    RuleFor(x => new { Id = x.MakerSpaceId, x.Make, x.Model }).MustAsync(async (req, cancellation) => 
                   {
                       return !(await _context.MakerSpace.Include(x => x.Tools)
                                        .FirstOrDefaultAsync(x => req.Id == x.Id))
                                        .Tools.Any(x => x.Make == req.Make && x.Model == req.Model);
                   }).WithMessage(x => $"Tool with make: \"{x.Make}\" and model: \"{x.Model}\" already exists on MakerSpace");
            });
        }
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
            var makerSpace = await _context.MakerSpace.Include(x => x.Tools).FirstOrDefaultAsync(x => x.Id == request.MakerSpaceId);
            var msTool = makerSpace.Tools.FirstOrDefault(x => x.Make == request.Make && x.Model == request.Model);
            var tool = new Tool { Make = request.Make, Model = request.Model };
            makerSpace.Tools.Add(tool);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}
