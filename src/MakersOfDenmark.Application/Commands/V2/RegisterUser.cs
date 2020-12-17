using MakersOfDenmark.Infrastructure.Persistence;
using MakersOfDenmark.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakersOfDenmark.Application.Commands.V2
{
    public class RegisterUser : IRequest<RegisterUserResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string SchoolName { get; set; }
    }
    public class RegisterUserHandler : IRequestHandler<RegisterUser, RegisterUserResponse>
    {
        private readonly MODContext _context;

        public RegisterUserHandler(MODContext context)
        {
            _context = context;
        }
        public async Task<RegisterUserResponse> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            var user = ConvertCommandToUser(request);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var response = new RegisterUserResponse { Id = user.Id };
            return response;
        }
        private User ConvertCommandToUser(RegisterUser command)
        => new User
        {
            Name = command.Name,
            Email = command.Email,
            UserName = command.UserName,
            Password = command.Password,
            Phone = command.Phone,
            Gender = command.Gender,
            Birthday = command.Birthday,
            SchoolName = command.SchoolName
        };
    }
    public class RegisterUserResponse
    {
        public Guid Id { get; set; }
    }
}
