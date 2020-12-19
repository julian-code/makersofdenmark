using MakersOfDenmark.Domain;
using MakersOfDenmark.Domain.Enums;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Queries.V1
{
    public class GetMakerSpaceById : IRequest<GetMakerSpaceByIdResponse>
    {
        public GetMakerSpaceById(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    public class GetMakerSpaceByIdHandler : IRequestHandler<GetMakerSpaceById, GetMakerSpaceByIdResponse>
    {
        private readonly MODContext _context;

        public GetMakerSpaceByIdHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<GetMakerSpaceByIdResponse> Handle(GetMakerSpaceById request, CancellationToken cancellationToken = default)
        {
            var makerSpace = await _context.MakerSpace.AsNoTracking()
                .Include(x => x.Address)
                .Include(x => x.ContactInfo)
                .Include(x => x.Members)
                .Include(x => x.Tools)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (makerSpace is null)
            {
                return null;
            }

            return new GetMakerSpaceByIdResponse(makerSpace);
        }
    }

    public class GetMakerSpaceByIdResponse : MakerSpaceViewmodel
    {
        public GetMakerSpaceByIdResponse(MakerSpace ms)
        {
            Id = ms.Id;
            Name = ms.Name;
            WorkShopType = ms.WorkShopType;
            Description = ms.Description;
            if (!(ms.Address is null))
            {
                Address = AddressViewmodel.Create(ms.Address);
            }
            if (!(ms.ContactInfo is null))
            {
                ContactInformation = ContactInformationViewModel.Create(ms.ContactInfo);
            }
            VATNumber = ms.VATNumber;
            LogoUrl = ms.Logo.ToString();
            AccessType = ms.AccessType;
            if (!string.IsNullOrWhiteSpace(ms.Organization))
            {
                Organization = ms.Organization;
            }
            Tools = ms.Tools.Select(x => x.Name).ToList();
            Members = ms.Members.Select(x => new UserVM { UserName = x.UserName, SchoolName = x.SchoolName }).ToList();
        }
        public List<UserVM> Members { get; set; } = new List<UserVM>();        
    }

    public class UserVM
    {
        public string UserName { get; set; }
        public string SchoolName { get; set; }
    }


    public class ToolViewModel
    {
        public string Make { get; set; }
        public string Model { get; set; }

        public ToolViewModel(Tool tool)
        {
            Make = tool.Make;
            Model = tool.Model;
        }
    }
}
