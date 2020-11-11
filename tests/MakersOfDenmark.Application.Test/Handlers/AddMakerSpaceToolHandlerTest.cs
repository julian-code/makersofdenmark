using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Commands.V1.admin;
using MakersOfDenmark.Domain.Models;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.Application.Tests.Handlers
{
    public class AddMakerSpaceToolHandlerTest : IClassFixture<RequestFixture>
    {
        private readonly RequestFixture _requestHandlerFixture;

        public AddMakerSpaceToolHandlerTest(RequestFixture requestHandlerFixture)
        {
            _requestHandlerFixture = requestHandlerFixture;
        }

        [Fact]
        public async Task AddToolToMakerSpaceTest()
        {
            //Arrange
            _requestHandlerFixture.FixtureRecursionConfiguration();
            var makerSpace = _requestHandlerFixture.Fixture.Build<MakerSpace>().Without(x => x.Tools).With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode")).Create();
            _requestHandlerFixture.DbContext.MakerSpace.Add(makerSpace);
            await _requestHandlerFixture.DbContext.SaveChangesAsync();

            //Act
            var handler = new AddMakerSpaceToolsHandler(_requestHandlerFixture.DbContext);
            var req = _requestHandlerFixture.Fixture.Build<AddMakerSpaceTool>().With(x => x.MakerSpaceId, makerSpace.Id).Create();

            //Assert
            makerSpace.Tools.Should().HaveCount(0);

            await handler.Handle(req);
            makerSpace.Tools.Should().HaveCount(1);

        }

    }
}
