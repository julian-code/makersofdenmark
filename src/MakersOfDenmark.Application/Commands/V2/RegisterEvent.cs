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
    public class RegisterEvent : IRequest<RegisterEventResponse>
    {
        public Guid MakerSpaceId { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Description { get; set; }
        public string Badge { get; set; }
        public MakerSpace MakerSpace { get; set; }
    }

    public class RegisterEventHandler : IRequestHandler<RegisterEvent, RegisterEventResponse>
    {
        private readonly MODContext _context;

        public RegisterEventHandler(MODContext context)
        {
            _context = context;
        }

        public async Task<RegisterEventResponse> Handle(RegisterEvent request, CancellationToken cancellationToken)
        {
            var newEvent = ConvertCommandToEvent(request);
            _context.Events.Add(newEvent);

            await _context.SaveChangesAsync();
            var response = new RegisterEventResponse { Id = newEvent.Id };
            return response;
        }

        private Event ConvertCommandToEvent(RegisterEvent command)
        => new Event
        {
            Title = command.Title,
            Description = command.Description,
            Start = command.Start,
            End = command.End,
            Badge = command.Badge,
            Address = command.Address,
            //MakerSpace = command.MakerSpace,
            MakerSpaceId = command.MakerSpaceId
        };
        
    }
    public class RegisterEventResponse
    {
        public Guid Id { get; set; }
    }
}
