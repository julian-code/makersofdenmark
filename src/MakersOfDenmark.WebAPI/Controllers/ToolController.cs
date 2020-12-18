using MakersOfDenmark.Application.Commands.V1.admin;
using MakersOfDenmark.Application.Commands.V2.Makerspace;
using MakersOfDenmark.Application.Queries.V1;
using MakersOfDenmark.Application.Queries.V2.Makerspace;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MakersOfDenmark.WebAPI.Controllers
{

    [ApiController]
    public class ToolController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly MODContext _context;

        public ToolController(IMediator mediator, MODContext context)
        {
            _mediator = mediator;
            _context = context;
        }
        [ProducesResponseType(typeof(GetMakerSpaceByToolsByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("makerspace/{makerSpaceId}/tools")]
        public async Task<IActionResult> GetTools(Guid makerSpaceId)
        {
            var response = await _mediator.Send(new GetMakerSpaceToolsById(makerSpaceId));

            if (response is null)
            {
                return NotFound(makerSpaceId);
            }

            return Ok(response);
        }
        [HttpPost("MakerSpace/{MakerSpaceId}/tools")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<IActionResult> AddMakerSpaceTools(AddMakerSpaceTool request)
        {
            await _mediator.Send(request);
            return CreatedAtAction(nameof(GetTools), new { makerSpaceId = request.MakerSpaceId }, null);
        }

        [HttpDelete("tools/{toolId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<IActionResult> RemoveMakerSpaceTools(RemoveMakerSpaceTool request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut("tools/{toolId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMakerSpaceTools(UpdateMakerSpaceTool request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
