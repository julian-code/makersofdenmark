using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Commands.V2
{
    public class RemoveUser : IRequest
    {
        public RemoveUser(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
    public class RemoveUserHandler : IRequestHandler<RemoveUser>
    {
        private readonly MODContext _context;

        public RemoveUserHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(RemoveUser request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}
