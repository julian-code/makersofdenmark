using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Queries.V1;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.WebAPI.Tests
{
    public class GetMakerSpaceByIdHandlerTests : RequestHandlerTest
    {
        [Fact]
        public async Task GetMakerSpaceByIdTest()
        {
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var actual = _fixture.Build<MakerSpace>().With(x => x.Address, new Address("Test Street", "1", "Test City", "Test Country", "Test Postcode", "Floor")).Create();

            using var dbContext = new MODContext(_options);

            _dbContext.MakerSpace.Add(actual);
            await _dbContext.SaveChangesAsync();

            var handler = new GetMakerSpaceByIdHandler(dbContext);

            var result = await handler.Handle(new GetMakerSpaceById(actual.Id));

            result.Id.Should().Be(actual.Id);
            result.Organization.Should().Be(actual.Organization.Name);
            result.Tools.Should().HaveCount(actual.Tools.Count);
            result.Tools.Select(x => x.Name).Should().BeEquivalentTo(actual.Tools.Select(x => x.Name));
            result.Address.Should().Be(actual.Address.FullAddress);
            result.Tools.First().Categories.Should().NotBeNull().And.HaveCount(actual.Tools.First().Categories.Count);
        }
    }
}
