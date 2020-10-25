using FluentValidation;
using MakersOfDenmark.Application.Commands.Validators;
using MakersOfDenmark.Domain.Enums;
using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Commands.V1.admin
{
    public class EditBaseMakerSpace : IHaveMakerSpaceIdentifier, IHaveBaseMakerSpace, IRequest
    {
        public Guid MakerSpaceId { get; set; }
        public string Name { get; set; }
        public string VATNumber { get; set; }
        public string LogoUrl { get; set; }
        public AccessType AccessType { get; set; }
    }
    public class EditBaseMakerSpaceValidator :  AbstractValidator<EditBaseMakerSpace>
    {
        private readonly MODContext _context;

        public EditBaseMakerSpaceValidator(MODContext context)
        {
            _context = context;
            Include(new MakerSpaceIdentifierValidator(_context));
            Include(new BaseMakerSpaceValidator());
        }
    }

    public class EditBaseMakerSpaceHandler : IRequestHandler<EditBaseMakerSpace>
    {
        private readonly MODContext _context;

        public EditBaseMakerSpaceHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(EditBaseMakerSpace request, CancellationToken cancellationToken = default)
        {
            var makerSpace = await _context.MakerSpace.FirstOrDefaultAsync(x => x.Id == request.MakerSpaceId);
            if (makerSpace is null)
            {
                throw new NullReferenceException("Cannot find MakerSpace");
            }
            makerSpace.Name = request.Name;
            makerSpace.VATNumber = request.VATNumber;
            makerSpace.Logo = new Uri(request.LogoUrl);
            makerSpace.AccessType = request.AccessType;
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}
