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

namespace MakersOfDenmark.WebAPI.Tests.Controllers
{
    public class MakerSpaceControllerTest
    {
        [Fact]
        public async Task Get_SendsCorrectInput()
        {
            // Configuration 
            var mediator = new Mock<IMediator>();
            GetMakerSpaceById actual = null;
            mediator.Setup(m => m.Send(It.IsAny<GetMakerSpaceById>(), It.IsAny<CancellationToken>()))
                .Callback((IRequest<GetMakerSpaceByIdResponse> req, CancellationToken token) => { actual = req as GetMakerSpaceById; })
                .ReturnsAsync(It.IsAny<GetMakerSpaceByIdResponse>);

            // Arrange
            Guid expectedId = Guid.NewGuid();
            var msCont = new MakerSpaceController(mediator.Object);

            // Act
            var result = await msCont.Get(expectedId);
            
            // Assert
            actual.Id.Should().Be(expectedId);
        }
        [Fact]
        public async Task Get_InvalidId_ReturnsStatus404()
        {
            // Configuration
            var mediator = new Mock<IMediator>();
            GetMakerSpaceByIdResponse getMSByIdResponse = null;
            mediator.Setup(m => m.Send(It.IsAny<GetMakerSpaceById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(getMSByIdResponse);

            // Arrange
            int expectedStatusCode = (int)HttpStatusCode.NotFound;
            Guid id = Guid.NewGuid();
            var msCont = new MakerSpaceController(mediator.Object);

            // Act
            var response = await msCont.Get(id);
            var actual = response as NotFoundObjectResult;

            // Assert
            actual.StatusCode.Should().Be(expectedStatusCode);

            Assert.Equal(expectedStatusCode, actual.StatusCode);
        }
        [Fact]
        public async Task Get_ValidId_ReturnsStatus200()
        {
            // Configuration
            var mediator = new Mock<IMediator>();
            var fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var fakeResponse = fixture.Create<GetMakerSpaceByIdResponse>();
            mediator.Setup(m => m.Send(It.IsAny<GetMakerSpaceById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(fakeResponse);

            // Arrange
            int expectedStatusCode = (int)HttpStatusCode.OK;
            
            var msCont = new MakerSpaceController(mediator.Object);

            // Act
            var response = await msCont.Get(Guid.NewGuid());
            var actual = response as OkObjectResult;

            // Assert
            actual.StatusCode.Should().Be(expectedStatusCode);
        }
        [Fact]
        public async Task Get_CallsSendOnce()
        {
            // Configure
            var mediator = new Mock<IMediator>();
            // Arrange
            Guid id = Guid.NewGuid();
            var controller = new MakerSpaceController(mediator.Object);
            // Act
            var response = controller.Get(id);
            // Assert
            mediator.Verify(m => m.Send(It.IsAny<GetMakerSpaceById>(),default), Times.Once);
        }
    }
}
