using MakersOfDenmark.Application.Commands.V1.admin;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MakersOfDenmark.WebAPI.Controllers
{
    // TODO:
    // Only users with MakerSpace Admin should be able to use these endpoints
    [ApiController]
    public class AdminMakerSpaceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminMakerSpaceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("MakerSpace/{MakerSpaceId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> EditMakerSpace(EditBaseMakerSpace request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("MakerSpace/{MakerSpaceId}/address")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> EditMakerSpaceAddress(EditMakerSpaceAddress request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("MakerSpace/{MakerSpaceId}/contactinformation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> EditMakerSpaceContactInformation(EditMakerSpaceContactInfo request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("MakerSpace/{MakerSpaceId}/organization")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> EditMakerSpaceOrganization(EditMakerSpaceOrganization request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("MakerSpace/{MakerSpaceId}/tools")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddMakerSpaceTools(AddMakerSpaceTool request)
        {
            await _mediator.Send(request);
            return CreatedAtAction(nameof(MakerSpaceController.GetTools), "MakerSpace", new { makerSpaceId = request.MakerSpaceId }, request);
        }

        [HttpDelete("MakerSpace/{MakerSpaceId}/tools/{toolId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoveMakerSpaceTools(RemoveMakerSpaceTool request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
