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
    public class GetUserById : IRequest<GetUserByIdResponse>
    {
        public Guid Id { get; set; }
        public GetUserById(Guid id)
        {
            Id = id;
        }
    }
    public class GetUserByIdHandler : IRequestHandler<GetUserById, GetUserByIdResponse>
    {
        private readonly MODContext _context;

        public GetUserByIdHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<GetUserByIdResponse> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(x => x.MakerSpaces).Include(x => x.Badges).FirstOrDefaultAsync(x => x.Id == request.Id);
            var response = GetUserByIdResponse.CreateResponse(user);
            return response;
        }
    }

    public class GetUserByIdResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string SchoolName { get; set; }
        public ICollection<Guid> MakerSpaces { get; set; } = new List<Guid>();
        public ICollection<Guid> Badges { get; set; } = new List<Guid>();
        public static GetUserByIdResponse CreateResponse(User user)
        {
            var response = new GetUserByIdResponse
            {
                Name = user.Name,
                Email = user.Email,
                UserName = user.UserName,
                Phone = user.Phone,
                Gender = user.Gender,
                Birthday = user.Birthday,
                SchoolName = user.SchoolName
            };
            user.MakerSpaces.ToList().ForEach(x => response.MakerSpaces.Add(x.Id));
            user.Badges.ToList().ForEach(x => response.Badges.Add(x.Id));
            return response;
        }
    }
}
