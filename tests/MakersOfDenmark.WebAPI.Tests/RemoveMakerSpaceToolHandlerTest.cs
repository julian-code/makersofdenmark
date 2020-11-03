using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Commands.V1.admin;
using MakersOfDenmark.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.WebAPI.Tests
{
    public class RemoveMakerSpaceToolHandlerTest : IClassFixture<RequestHandlerFixture>
    {
        private readonly RequestHandlerFixture _requestHandlerFixture;

        public RemoveMakerSpaceToolHandlerTest(RequestHandlerFixture requestHandlerFixture)
        {
            _requestHandlerFixture = requestHandlerFixture;
        }

        [Fact]
        public async Task RemoveToolToMakerSpaceTest()
        {
            //Configuration
            _requestHandlerFixture.FixtureRecursionConfiguration();

            //Arrange
            var makerSpace = _requestHandlerFixture.Fixture.Build<MakerSpace>().Without(x => x.Tools).With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode")).Create();
            
            var tool = _requestHandlerFixture.Fixture.Build<Tool>().Without(x => x.MakerSpaces).Without(x => x.Categories).Create();
            makerSpace.Tools.Add(tool);
            
            _requestHandlerFixture.DbContext.MakerSpace.Add(makerSpace);
            await _requestHandlerFixture.DbContext.SaveChangesAsync();

            //Act
            var handler = new RemoveMakerSpaceToolsHandler(_requestHandlerFixture.DbContext);
            var req = new RemoveMakerSpaceTool { MakerSpaceId = makerSpace.Id, ToolId = tool.Id };

            //Assert
            makerSpace.Tools.Should().HaveCount(1);

            await handler.Handle(req);
            makerSpace.Tools.Should().HaveCount(0);

        }

    }
}
