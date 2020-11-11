using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Commands.V1.admin;
using MakersOfDenmark.Domain.Models;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.Application.Tests.Handlers
{
    public class RemoveMakerSpaceToolHandlerTest : IClassFixture<RequestFixture>
    {
        private readonly RequestFixture _requestFixture;

        public RemoveMakerSpaceToolHandlerTest(RequestFixture requestFixture)
        {
            _requestFixture = requestFixture;
        }

        [Fact]
        public async Task RemoveToolToMakerSpaceTest()
        {
            // Configuration
            _requestFixture.FixtureRecursionConfiguration();

            // Arrange
            var makerSpace = _requestFixture.Fixture.Build<MakerSpace>().Without(x => x.Tools).With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode")).Create();
            
            var tool = _requestFixture.Fixture.Build<Tool>().Without(x => x.MakerSpaces).Without(x => x.Categories).Create();
            makerSpace.Tools.Add(tool);
            
            _requestFixture.DbContext.MakerSpace.Add(makerSpace);
            await _requestFixture.DbContext.SaveChangesAsync();

            // Act
            var handler = new RemoveMakerSpaceToolsHandler(_requestFixture.DbContext);
            var req = new RemoveMakerSpaceTool { MakerSpaceId = makerSpace.Id, ToolId = tool.Id };

            // Assert
            makerSpace.Tools.Should().HaveCount(1);
            await handler.Handle(req);
            makerSpace.Tools.Should().HaveCount(0);

        }

    }
}
