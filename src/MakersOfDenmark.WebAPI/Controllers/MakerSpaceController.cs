using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        [ProducesResponseType(typeof(GetMakerSpaceByIdResponse), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Guid id)
        {
            return Ok(await _mediator.Send(new GetMakerSpaceById(id)));
        }

        [ProducesResponseType(typeof(GetMakerSpaceByToolsByIdResponse), StatusCodes.Status200OK)]
        [HttpGet("{makerSpaceId}/tools")]
        public async Task<IActionResult> GetTools(Guid makerSpaceId)
        {
            return Ok(await _mediator.Send(new GetMakerSpaceToolsById(makerSpaceId)));
        }
    }
}
