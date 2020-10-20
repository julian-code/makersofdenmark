﻿using MakersOfDenmark.Domain;
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
                .Include(x => x.Organization)
                .Include(x => x.MakerSpaceType)
                .Include(x => x.ContactInfo)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (makerSpace is null)
            {
                return null;
            }

            return new GetMakerSpaceByIdResponse(makerSpace);
        }
    }

    public class GetMakerSpaceByIdResponse
    {
        public string Address { get; set; }
        public string Organization { get; set; }
        public string AccesType { get; set; }
        public string[] ContactInfo { get; set; }
        public string Logo { get; set; }
        public string VatNumber { get; set; }
        public string MakerSpaceType { get; set; }

        public GetMakerSpaceByIdResponse(MakerSpace makerSpace)
        {
            Address = makerSpace.Address.FullAddress;
            Organization = makerSpace.Organization.Name;
            AccesType = makerSpace.AccessType.ToString();
            ContactInfo = new string[] { makerSpace.ContactInfo.Phone, makerSpace.ContactInfo.Email };
            Logo = makerSpace.Logo.ToString();
            VatNumber = makerSpace.VATNumber;
            MakerSpaceType = makerSpace.MakerSpaceType.Name;
        }
    }

    public class ToolViewModel
    {
        public string Name { get; set; }
        public string[] Categories { get; set; }

        public ToolViewModel(Tool tool)
        {
            Categories = tool.Categories.Select(x => x.Title).ToArray();
            Name = tool.Name;
        }
    }
}