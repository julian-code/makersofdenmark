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

        [HttpGet]
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
        
        [HttpDelete]
    }
}
