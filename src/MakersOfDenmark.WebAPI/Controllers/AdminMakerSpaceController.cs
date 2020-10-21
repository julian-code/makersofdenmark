using MakersOfDenmark.Application.Commands.V1.admin;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MakersOfDenmark.WebAPI.Controllers
{
    [ApiController]
    public class AdminMakerSpaceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminMakerSpaceController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPut("MakerSpace/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> EditMakerSpace(EditBaseMakerSpace request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut("MakerSpace/{id}/address")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> EditMakerSpaceAddress(EditMakerSpaceAddress request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut("MakerSpace/{id}/contactinformation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> EditMakerSpaceContactInformation(EditMakerSpaceContactInfo request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut("MakerSpace/{id}/organization")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> EditMakerSpaceOrganization(EditMakerSpaceOrganization request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut("MakerSpace/{id}/tools")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddMakerSpaceTools(AddMakerSpaceTool request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete("MakerSpace/{id}/tools/{toolId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoveMakerSpaceTools(RemoveMakerSpaceTool request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
