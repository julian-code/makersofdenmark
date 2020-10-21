using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MakersOfDenmark.WebAPI.Controllers
{
    [ApiController]
    public class AdminMakerSpaceController : ControllerBase
    {
        [HttpPatch("makerspace/{id}")]
        public async Task<IActionResult> EditMakerSpace()
        {
            throw new NotImplementedException();
        }
        [HttpPatch("makerspace/{id}/address")]
        public async Task<IActionResult> EditMakerSpaceAddress()
        {
            throw new NotImplementedException();
        }
        [HttpPatch("makerspace/{id}/contactinformation")]
        public async Task<IActionResult> EditMakerSpaceContactInformation()
        {
            throw new NotImplementedException();
        }
        [HttpPatch("makerspace/{id}/organization")]
        public async Task<IActionResult> EditMakerSpaceOrganization()
        {
            throw new NotImplementedException();
        }
    }
}
