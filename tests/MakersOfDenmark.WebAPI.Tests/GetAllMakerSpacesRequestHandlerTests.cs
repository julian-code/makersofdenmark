using AutoFixture;
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
            var makerSpaces = _fixture.CreateMany<MakerSpace>();

            using var dbContext = new MODContext(_options);
            _dbContext.AddRange(makerSpaces);

            var sut = new GetAllMakerSpacesRequestHandler(dbContext);

            //Act
            var result = await sut.Handle(new GetAllMakerSpaces());

            //Assert
            Assert.True(result.SequenceEqual(makerSpaces));
        }
    }
}
