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

        private readonly MODContext _context;
        public BadgeController(MODContext MODContext)
        {
            _context = MODContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBadges()
        {
            var response = await _context.Badges.ToListAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBadgeById(Guid id)
        {
            var response = await _context.Badges.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterBadge(Badge badge)
        {
            await _context.Badges.AddAsync(badge);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBadgeById), new { id = badge.Id }, badge);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateBadge(Guid id, Badge badge)
        {
            var badgeToUpdate = await _context.Badges.FirstOrDefaultAsync(x => x.Id == id);
            if (badgeToUpdate.Id != badge.Id)
            {
                throw new Exception("Id-update is not allowed");
            }
            badgeToUpdate.Name = badge.Name;
            badgeToUpdate.Icon = badge.Icon;
            badgeToUpdate.Description = badge.Description;
            _context.Entry(badge).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBadge(Guid id)
        {

            var entity = await _context.Badges.FirstOrDefaultAsync(x => x.Id == id);
            _context.Badges.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
