using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MakersOfDenmark.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly MODContext _MODContext;
        public EventController(MODContext MODContext)
        {
            _MODContext = MODContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventsForMakerSpace(Guid makerSpaceId)
        {
            var makerSpace = await _MODContext.MakerSpace.FirstOrDefaultAsync(x => x.Id == makerSpaceId);
            var response = makerSpace.Events;
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventById(Guid makerSpaceId, Guid eventId)
        {
            var makerSpace = await _MODContext.MakerSpace.FirstOrDefaultAsync(x => x.Id == makerSpaceId);
            var response = makerSpace.Events.FirstOrDefault(x => x.Id == eventId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(Guid makerSpaceId, Event newEvent)
        {
            var makerSpace = await _MODContext.MakerSpace.FirstOrDefaultAsync(x => x.Id == makerSpaceId);
            newEvent.MakerSpace = makerSpace;
            makerSpace.Events.Add(newEvent);

            await _MODContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEventById), new { id = newEvent.Id }, newEvent);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent(Guid makerSpaceId, Guid eventId, Event newEvent)
        {
            var makerSpace = await _MODContext.MakerSpace.FirstOrDefaultAsync(x => x.Id == makerSpaceId);
            var eventToUpdate = makerSpace.Events.FirstOrDefault(x => x.Id == eventId);
            eventToUpdate.Address = newEvent.Address;
            eventToUpdate.Title = newEvent.Title;
            eventToUpdate.Start = newEvent.Start;
            eventToUpdate.End = newEvent.End;
            eventToUpdate.Description = newEvent.Description;
            eventToUpdate.Badge = newEvent.Badge;
            _MODContext.Entry(eventToUpdate).State = EntityState.Modified;
            await _MODContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEvent(Guid makerSpaceId, Guid eventId)
        {
            var makerSpace = await _MODContext.MakerSpace.FirstOrDefaultAsync(x => x.Id == makerSpaceId);
            var eventToDelete = makerSpace.Events.FirstOrDefault(x => x.Id == eventId);
            makerSpace.Events.Remove(eventToDelete);
            await _MODContext.SaveChangesAsync();
            return NoContent();
        }
}
