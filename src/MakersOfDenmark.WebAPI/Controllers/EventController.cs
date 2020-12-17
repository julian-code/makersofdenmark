using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetAllEvents()
        {
            var response = await _MODContext.Events.ToListAsync();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventById(Guid id)
        {
            var response = await _MODContext.Events.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(response);
        }
    }
}
