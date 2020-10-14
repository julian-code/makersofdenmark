using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Queries.V1;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.WebAPI.Tests
{
    public class GetAllMakerSpacesRequestHandlerTests
    {
        private readonly DbContextOptions<MODContext> _options;
        private readonly MODContext _dbContext;
        private readonly Fixture _fixture;

        public GetAllMakerSpacesRequestHandlerTests()
        {
            _options = new DbContextOptionsBuilder<MODContext>()
                .UseInMemoryDatabase(databaseName: "MakersOfDenmarkDatabase")
                .Options;

            _dbContext = new MODContext(_options);

            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetAllTest()
        {
            //Arrange
            var makerSpaces = _fixture.Build<MakerSpace>().Without(x => x.Tools).CreateMany();

            using var dbContext = new MODContext(_options);
            _dbContext.AddRange(makerSpaces);
            await _dbContext.SaveChangesAsync();

            var handler = new GetAllMakerSpacesRequestHandler(dbContext);

            //Act
            var result = await handler.Handle(new GetAllMakerSpaces());

            //Assert
            result.Should().HaveCount(makerSpaces.Count());
        }
    }
}
