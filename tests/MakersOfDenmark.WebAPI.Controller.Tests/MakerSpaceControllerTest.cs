using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Queries.V1;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.WebAPI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.WebAPI.Controller.Tests
{
    public class MakerSpaceControllerTest
    {
        [Fact]
        public async Task Get_InvalidId_ReturnsStatus404()
        {
            // arrange
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetMakerSpaceById>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<GetMakerSpaceByIdResponse>);
            var msCont = new MakerSpaceController(mediator.Object);
            
            // act
            var result = await msCont.Get(Guid.NewGuid());
            NotFoundObjectResult realResult = result as NotFoundObjectResult;

            // assert
            realResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task Get_ValidId_ReturnsStatus200()
        {
            // arrange
            var mediator = new Mock<IMediator>();
            var fixture = new Fixture();
            var makerspace = fixture.Build<MakerSpace>().Without(x=>x.Organization).Without(x=>x.Tools).Create();
            var response = new GetMakerSpaceByIdResponse(makerspace);
            mediator.Setup(m => m.Send(It.IsAny<GetMakerSpaceById>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);
            var msCont = new MakerSpaceController(mediator.Object);

            // act
            var result = await msCont.Get(Guid.NewGuid());
            OkObjectResult realResult = result as OkObjectResult;

            // assert
            realResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
    }
}
