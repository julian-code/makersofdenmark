using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakersOfDenmark.Application.Queries.V1;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakersOfDenmark.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
