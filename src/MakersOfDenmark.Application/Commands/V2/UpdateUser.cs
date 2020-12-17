using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Commands.V2
{
    public class UpdateUser : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string SchoolName { get; set; }
    }
    public class UpdateUserHandler : IRequestHandler<UpdateUser>
    {
        private readonly MODContext _context;

        public UpdateUserHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id);
            user.UpdateUserFromRequest(request);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Unit();
        }

        
    }
    public static class UserExtension {
        public static User UpdateUserFromRequest(this User user, UpdateUser request)
        {
            if (!string.IsNullOrWhiteSpace(request.Name)) { user.Name = request.Name; }
            if (!string.IsNullOrWhiteSpace(request.Email)) { user.Email = request.Email; }
            if (!string.IsNullOrWhiteSpace(request.UserName)) { user.UserName = request.UserName; }
            if (!string.IsNullOrWhiteSpace(request.Password)) { user.Password = request.Password; }
            if (!string.IsNullOrWhiteSpace(request.Phone)) { user.Phone = request.Phone; }
            if (!string.IsNullOrWhiteSpace(request.Gender)) { user.Gender = request.Gender; }
            if (request.Birthday != default) { user.Birthday = request.Birthday; }
            if (!string.IsNullOrWhiteSpace(request.SchoolName)) { user.SchoolName = request.SchoolName; }
    
            return user;
        }
    }
}
