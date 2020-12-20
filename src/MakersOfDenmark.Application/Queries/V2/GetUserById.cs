using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Queries.V2
{
    public class GetUserById : IRequest<GetAllUsersResponse>
    {
        public Guid Id { get; set; }
        public GetUserById(Guid id)
        {
            Id = id;
        }
    }
    public class GetUserByIdHandler : IRequestHandler<GetUserById, GetAllUsersResponse>
    {
        private readonly MODContext _context;

        public GetUserByIdHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<GetAllUsersResponse> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(x => x.MakerSpaces).Include(x => x.Badges).FirstOrDefaultAsync(x => x.Id == request.Id);
            var response = GetAllUsersResponse.CreateResponse(user);
            return response;
        }
    }

}
