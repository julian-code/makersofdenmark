using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MakersOfDenmark.Application.Commands.V1;
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
        public async Task<IActionResult> RegisterMakerSpace(RegisterMakerSpace request)
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

        [ProducesResponseType(typeof(GetMakerSpaceByToolsByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{makerSpaceId}/tools")]
        public async Task<IActionResult> GetTools(Guid makerSpaceId)
        {
            var response = await _mediator.Send(new GetMakerSpaceToolsById(makerSpaceId));

            if (response is null)
            {
                return NotFound(makerSpaceId);
            }

            return Ok(response);
        }

        [ProducesResponseType(typeof(GetMakerSpaceEventsByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{makerSpaceId}/events")]
        public async Task<IActionResult> GetEvents(Guid makerSpaceId)
        {
            throw new NotImplementedException();
        }
    }
}
