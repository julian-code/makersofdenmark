using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakersOfDenmark.Application.Commands.V2;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using MakersOfDenmark.WebAPI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MakersOfDenmark.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly MODContext _context;
        private readonly IMediator _mediator;
        public EventController(MODContext MODContext, IMediator mediator)
        {
            _context = MODContext;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventsForMakerSpace(Guid makerSpaceId)
        {
            var makerSpace = await _context.MakerSpace.Include(x => x.Events).FirstOrDefaultAsync(x => x.Id == makerSpaceId);
            return Ok(makerSpace.Events);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEventById(Guid eventId)
        {
            var response = await _context.Set<Event>().FirstOrDefaultAsync(x => x.Id == eventId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(RegisterEvent request)
        {
            //var makerSpace = await _context.MakerSpace.FirstOrDefaultAsync(x => x.Id == makerSpaceId);
            //makerSpace.AddEvent(newEvent);
            //await _context.SaveChangesAsync();
            //return Ok();
            var response = await _mediator.Send(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent(Guid makerSpaceId, Guid eventId, Event newEvent)
        {

            var makerSpace = await _context.MakerSpace.FirstOrDefaultAsync(x => x.Id == makerSpaceId);

            var eventToUpdate = makerSpace.Events.FirstOrDefault(x => x.Id == eventId);
            eventToUpdate.Address = newEvent.Address;
            eventToUpdate.Title = newEvent.Title;
            eventToUpdate.Start = newEvent.Start;
            eventToUpdate.End = newEvent.End;
            eventToUpdate.Description = newEvent.Description;
            eventToUpdate.Badge = newEvent.Badge;

            _context.Entry(eventToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEvent(Guid makerSpaceId, Guid eventId)
        {

            var makerSpace = await _context.MakerSpace.FirstOrDefaultAsync(x => x.Id == makerSpaceId);
            var eventToDelete = makerSpace.Events.FirstOrDefault(x => x.Id == eventId);
            makerSpace.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
