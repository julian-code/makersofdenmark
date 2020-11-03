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
        public async Task Get_SendsCorrectInput()
        {
            // arrange
            Guid expectedId = Guid.NewGuid();
            GetMakerSpaceById request = null;
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetMakerSpaceById>(), It.IsAny<CancellationToken>()))
                .Callback((IRequest<GetMakerSpaceByIdResponse> req, CancellationToken token) => { request = req as GetMakerSpaceById; })
                .ReturnsAsync(It.IsAny<GetMakerSpaceByIdResponse>);
            var msCont = new MakerSpaceController(mediator.Object);

            // act
            var result = await msCont.Get(expectedId);
            
            // assert
            request.Id.Should().Be(expectedId);
        }
        [Fact]
        public async Task Get_InvalidId_ReturnsStatus404()
        {
            // arrange
            int expectedStatusCode = (int)HttpStatusCode.NotFound;
            Guid id = Guid.NewGuid();
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetMakerSpaceById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(It.IsAny<GetMakerSpaceByIdResponse>);
            var msCont = new MakerSpaceController(mediator.Object);

            
            // act
            var response = await msCont.Get(id);
            var actual = response as NotFoundObjectResult;

            // assert
            actual.StatusCode.Should().Be(expectedStatusCode);
        }
        [Fact]
        public async Task Get_ValidId_ReturnsStatus200()
        {
            // arrange
            int expectedStatusCode = (int)HttpStatusCode.OK;
            var mediator = new Mock<IMediator>();
            var fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var fakeResponse = fixture.Create<GetMakerSpaceByIdResponse>();
            mediator.Setup(m => m.Send(It.IsAny<GetMakerSpaceById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(fakeResponse);
            var msCont = new MakerSpaceController(mediator.Object);

            // act
            var response = await msCont.Get(Guid.NewGuid());
            var actual = response as OkObjectResult;

            // assert
            actual.StatusCode.Should().Be(expectedStatusCode);
        }
    }
}
