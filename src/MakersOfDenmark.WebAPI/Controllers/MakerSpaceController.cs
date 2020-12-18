using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MakersOfDenmark.Application.Commands.V1;
using MakersOfDenmark.Application.Commands.V2.Makerspace;
using MakersOfDenmark.Application.Queries.V1;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakersOfDenmark.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MakerSpaceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MakerSpaceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllMakerSpaces()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterMakerSpace(RegisterMakerSpaceV2 request)
        {
            var newId = await _mediator.Send(request);
            return CreatedAtAction(nameof(Get), new { id = newId }, newId);
        }

        [ProducesResponseType(typeof(GetMakerSpaceByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _mediator.Send(new GetMakerSpaceById(id));

            if (response is null)
            {
                return NotFound(id);
            }

            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMakerSpace(Guid id, UpdateMakerSpace request)
        {
            request.Id = id;
            await _mediator.Send(request);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("search")]
        public async Task<IActionResult> SearchForMakerSpace([FromQuery] string name)
        {
            return Ok(await _mediator.Send(new SearchForMakerSpace(name)));
        }
        [HttpDelete("{makerSpaceId}")]
        public async Task<IActionResult> DeleteMakerSpace(Guid makerSpaceId)
        {
            return Ok(await _mediator.Send(new DeleteMakerSpace(makerSpaceId)));
        }

    }
}
