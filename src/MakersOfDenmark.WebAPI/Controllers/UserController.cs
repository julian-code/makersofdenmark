﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        //TODO: Implement RegisterUser endpoint
        [HttpPost]
        public async Task<IActionResult> RegisterUser () //(User user)
        {
            throw new NotImplementedException();
            //var newUser = await _mediator.Send(user);
            //return CreatedAtAction("RegisterUser", user);
        }
    }
}
