using FluentValidation;
using MakersOfDenmark.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace MakersOfDenmark.Application.Commands.Validators
{
    public interface IHaveMakerSpaceIdentifier
    {
        public Guid MakerSpaceId { get; set; }
    }

    public class MakerSpaceIdentifierValidator : AbstractValidator<IHaveMakerSpaceIdentifier>
    {
        private readonly MODContext _context;

        public MakerSpaceIdentifierValidator(MODContext context)
        {
            _context = context;
            RuleFor(x => x.MakerSpaceId).MustAsync(async (id, cancellationToken) => {
                var makerSpace = await _context.MakerSpace.FirstOrDefaultAsync(x => x.Id == id);
                return !(makerSpace is null);
            }).WithMessage("MakerSpace doesn't exist");
            _context = context;
        }
    }
}
