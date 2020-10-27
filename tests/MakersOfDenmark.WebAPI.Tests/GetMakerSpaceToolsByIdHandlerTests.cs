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

namespace MakersOfDenmark.WebAPI.Tests
{
    public class GetMakerSpaceToolsByIdHandlerTests : IClassFixture<RequestHandlerFixture>
    {
        private readonly RequestHandlerFixture _requestHandlerFixture;

        public GetMakerSpaceToolsByIdHandlerTests(RequestHandlerFixture requestHandlerFixture)
        {
            _requestHandlerFixture = requestHandlerFixture;
        }

        [Fact]
        public async Task GetMakerSpaceToolsByIdTest()
        {
            _requestHandlerFixture.FixtureRecursionConfiguration();

            var actual = _requestHandlerFixture.Fixture.Build<MakerSpace>().Create();

            _requestHandlerFixture.DbContext.MakerSpace.Add(actual);
            await _requestHandlerFixture.DbContext.SaveChangesAsync();

            var handler = new GetMakerSpaceToolsByIdHandler(_requestHandlerFixture.DbContext);

            var result = await handler.Handle(new GetMakerSpaceToolsById(actual.Id));

            result.Tools.Select(x => x.Name).Should().BeEquivalentTo(actual.Tools.Select(x => x.Name));
            result.Tools.SelectMany(x => x.Categories).Should().BeEquivalentTo(actual.Tools.SelectMany(x => x.Categories).Select(x => x.Title));
        }
    }
}
