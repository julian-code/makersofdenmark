using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Queries.V1;
using MakersOfDenmark.Domain.Models;
using System.Linq;
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
            _requestFixture.FixtureRecursionConfiguration();

            var actual = _requestFixture.Fixture.Build<MakerSpace>().Create();

            _requestFixture.DbContext.MakerSpace.Add(actual);
            await _requestFixture.DbContext.SaveChangesAsync();

            var handler = new GetMakerSpaceToolsByIdHandler(_requestFixture.DbContext);

            var result = await handler.Handle(new GetMakerSpaceToolsById(actual.Id));

            result.Tools.Select(x => x.Name).Should().BeEquivalentTo(actual.Tools.Select(x => x.Name));
            result.Tools.SelectMany(x => x.Categories).Should().BeEquivalentTo(actual.Tools.SelectMany(x => x.Categories).Select(x => x.Title));
        }
    }
}
