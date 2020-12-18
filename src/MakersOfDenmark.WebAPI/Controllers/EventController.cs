using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakersOfDenmark.Application.Commands.V2;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using MakersOfDenmark.WebAPI.ViewModels;
using MediatR;
using System.Text.Json;
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
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _context.Events.ToListAsync();
            return Ok(events);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEventById(Guid eventId)
        {
            var response = await _context.Events.FirstOrDefaultAsync(x => x.Id == eventId);
            return Ok(response);
        }


        [HttpGet("makerspace/{makerSpaceId}")]
        public async Task<IActionResult> GetAllEventsForMakerSpace(Guid makerSpaceId)
        {
            var response = await _context.Events.Where(x => x.MakerSpaceId == makerSpaceId).ToListAsync();
            return Ok(response);
        }

        [HttpPost("makerspace/{makerSpaceId}")]
        public async Task<IActionResult> CreateEvent(RegisterEvent request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateEvent(Guid eventId, Event newEvent)
        {

            var existingEvent = await _context.Events.FirstOrDefaultAsync(x => x.Id == eventId);

            existingEvent.Address = newEvent.Address;
            existingEvent.Title = newEvent.Title;
            existingEvent.Start = newEvent.Start;
            existingEvent.End = newEvent.End;
            existingEvent.Description = newEvent.Description;
            existingEvent.Badge = newEvent.Badge;

            _context.Entry(existingEvent).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEvent(Guid eventId)
        {

            var eventToDelete = _context.Events.FirstOrDefault(x => x.Id == eventId);
            var makerSpace = _context.MakerSpace.FirstOrDefault(x => x.Id == eventToDelete.MakerSpaceId);

            _context.Events.Remove(eventToDelete);
            makerSpace.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
