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
    public class GetAllMakerSpacesRequestHandlerTests : RequestHandlerTest
    {
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
