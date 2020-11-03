using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Queries.V1;
using MakersOfDenmark.Domain.Models;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.Application.Tests.Handlers
{
    public class GetMakerSpaceByIdHandlerTests : IClassFixture<RequestFixture>
    {
        private readonly RequestFixture _requestFixture;

        public GetMakerSpaceByIdHandlerTests(RequestFixture requestFixture)
        {
            _requestFixture = requestFixture;
        }

        [Fact]
        public async Task GetMakerSpaceByIdTest()
        {
            _requestFixture.FixtureRecursionConfiguration();

            var actual = _requestFixture.Fixture.Build<MakerSpace>().With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode")).Create();

            _requestFixture.DbContext.MakerSpace.Add(actual);
            await _requestFixture.DbContext.SaveChangesAsync();

            var handler = new GetMakerSpaceByIdHandler(_requestFixture.DbContext);

            var result = await handler.Handle(new GetMakerSpaceById(actual.Id));

            result.Name.Should().Be(actual.Name);
            result.Organization.Should().Be(actual.Organization.Name);
            result.Address.Should().Be(actual.Address.FullAddress);
            result.ContactInfo.Should().Contain(new string[] { actual.ContactInfo.Email, actual.ContactInfo.Phone });
            result.Logo.Should().Be(actual.Logo.ToString());
        }
        [Fact]
        public async Task GetMakerSpace_WhichDoesntHaveOrganization_ById()
        {
            _requestFixture.FixtureRecursionConfiguration();

            var actual = _requestFixture.Fixture.Build<MakerSpace>().Without(x => x.Organization).With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode")).Create();
            _requestFixture.DbContext.MakerSpace.Add(actual);
            await _requestFixture.DbContext.SaveChangesAsync();

            var handler = new GetMakerSpaceByIdHandler(_requestFixture.DbContext);

            var result = await handler.Handle(new GetMakerSpaceById(actual.Id));

            result.Organization.Should().Be(null);
            result.Address.Should().Be(actual.Address.FullAddress);
            result.ContactInfo.Should().Contain(new string[] { actual.ContactInfo.Email, actual.ContactInfo.Phone });
            result.Logo.Should().Be(actual.Logo.ToString());
        }
    }
}
