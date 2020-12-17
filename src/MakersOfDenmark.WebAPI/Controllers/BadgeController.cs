using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MakersOfDenmark.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MakersOfDenmark.Domain.Models;

namespace MakersOfDenmark.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadgeController : ControllerBase
    {
        private readonly MODContext _MODContext;
        public BadgeController(MODContext MODContext)
        {
            _MODContext = MODContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBadges()
        {
            var response = await _MODContext.Badges.ToListAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBadgeById(Guid id)
        {
            var response = await _MODContext.Badges.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterBadge(Badge badge)
        {
            await _MODContext.Badges.AddAsync(badge);
            await _MODContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBadgeById), new { id = badge.Id }, badge);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBadge(Guid id, Badge badge)
        {
            var badgeToUpdate = await _MODContext.Badges.FirstOrDefaultAsync(x => x.Id == id);
            badgeToUpdate.Name = badge.Name;
            badgeToUpdate.Icon = badge.Icon;
            badgeToUpdate.Description = badge.Description;
            _MODContext.Entry(badge).State = EntityState.Modified;
            await _MODContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBadge(Guid id)
        {
            var entity = await _MODContext.Badges.FirstOrDefaultAsync(x => x.Id == id);
            _MODContext.Badges.Remove(entity);
            await _MODContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
