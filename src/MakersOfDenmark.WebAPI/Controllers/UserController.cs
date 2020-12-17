using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakersOfDenmark.Application.Commands.V2;
using MakersOfDenmark.Application.Queries.V2;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakersOfDenmark.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
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

    }
}
