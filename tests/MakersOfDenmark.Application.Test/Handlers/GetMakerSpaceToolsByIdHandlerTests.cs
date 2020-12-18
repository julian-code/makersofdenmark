using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Queries.V1;
using MakersOfDenmark.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.Application.Tests.Handlers
{
    public class GetMakerSpaceToolsByIdHandlerTests : IClassFixture<RequestFixture>
    {
        private readonly RequestFixture _requestFixture;

        public GetMakerSpaceToolsByIdHandlerTests(RequestFixture requestFixture)
        {
            _requestFixture = requestFixture;
        }

        [Fact]
        public async Task GetMakerSpaceToolsByIdTest()
        {
            //Configuration
            _requestFixture.FixtureRecursionConfiguration();

            //Arrange
            var actual = _requestFixture.Fixture.Build<MakerSpace>().Create();

            _requestFixture.DbContext.MakerSpace.Add(actual);
            await _requestFixture.DbContext.SaveChangesAsync();

            //Act
            var handler = new GetMakerSpaceToolsByIdHandler(_requestFixture.DbContext);

            var result = await handler.Handle(new GetMakerSpaceToolsById(actual.Id));

            //Assert
            result.Tools.Select(x => x.Make).Should().BeEquivalentTo(actual.Tools.Select(x => x.Make));
            
        }
    }
}
