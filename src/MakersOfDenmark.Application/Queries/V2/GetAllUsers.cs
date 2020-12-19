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

namespace MakersOfDenmark.Application.Queries.V2
{
    public class GetAllUsers : IRequest<List<GetAllUsersResponse>>
    {
    }

    public class GetAllUsersHandler : IRequestHandler<GetAllUsers, List<GetAllUsersResponse>>
    {
        private readonly MODContext _context;

        public GetAllUsersHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<List<GetAllUsersResponse>> Handle(GetAllUsers request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.Include(x=>x.Badges).Include(x=>x.MakerSpaces).AsNoTracking().ToListAsync();
            var response = new List<GetAllUsersResponse>();
            users.ForEach(x => response.Add(GetAllUsersResponse.CreateResponse(x)));
            return response;
        }
    }
    public class GetAllUsersResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string SchoolName { get; set; }
        public List<BadgeVM> Badges { get; set; } = new List<BadgeVM>();
        public List<MakerSpaceVM> MakerSpaces { get; set; } = new List<MakerSpaceVM>();

        public static GetAllUsersResponse CreateResponse(User user)
        {
            var msvm = user.MakerSpaces.Select(x=> new MakerSpaceVM { Id = x.Id, Name = x.Name, Description = x.Description, WorkShopType = x.WorkShopType});
            var bvm = user.Badges.Select(x=> new BadgeVM { Id = x.Id, Name = x.Name, Description = x.Description, Icon = x.Icon});
            return new GetAllUsersResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                UserName = user.UserName,
                Phone = user.Phone,
                Gender = user.Gender,
                Birthday = user.Birthday,
                SchoolName = user.SchoolName,
                Badges = bvm.ToList(),
                MakerSpaces = msvm.ToList()
            };
        }
    }

    public class MakerSpaceVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string WorkShopType { get; set; }
        public string Description { get; set; }
    }
    public class BadgeVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
    }
}
