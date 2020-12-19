using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakersOfDenmark.Application.Commands.V2;
using MakersOfDenmark.Application.Queries.V2;
using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MakersOfDenmark.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly MODContext _context;

        public UserController(IMediator mediator, MODContext context)
        {
            _mediator = mediator;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUser request) //(User user)
        {
            var response = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllUsers());
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetUserById(id));
            if (response is null)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUserById(Guid id)
        {
            await _mediator.Send(new RemoveUser(id));
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserById(Guid id, UpdateUser request)
        {
            if (!(request.Id == default))
            {
                if (request.Id != id)
                {
                    return BadRequest($"unclear which id to use: {id} or {request.Id}");
                }
            }
            request.Id = id;
            await _mediator.Send(request);
            return NoContent();
        }
        [HttpPost("{userId}/makerSpace/{makerspaceId}")]
        public async Task<IActionResult> UserFollowsMakerSpace(Guid userId, Guid makerspaceId)
        {
            var user = await _context.Users.Include(x => x.MakerSpaces).FirstOrDefaultAsync(x => x.Id == userId);
            var ms = await _context.MakerSpace.FirstOrDefaultAsync(x => x.Id == makerspaceId);
            if (user is null || ms is null)
            {
                return NotFound();
            }
            if (user.MakerSpaces.Contains(ms))
            {
                return NotFound("User already follows makerspace");
            }
            user.MakerSpaces.Add(ms);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Accepted();
        }
        [HttpDelete("{userId}/makerSpace/{makerspaceId}")]
        public async Task<IActionResult> UserUnfollowsMakerSpace(Guid userId, Guid makerspaceId)
        {
            var user = await _context.Users.Include(x => x.MakerSpaces).FirstOrDefaultAsync(x => x.Id == userId);
            var ms = await _context.MakerSpace.FirstOrDefaultAsync(x => x.Id == makerspaceId);
            if (user is null || ms is null)
            {
                return NotFound();
            }
            if (!user.MakerSpaces.Contains(ms))
            {
                return NotFound("User does not follow makerspace");
            }
            user.MakerSpaces.Remove(ms);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
