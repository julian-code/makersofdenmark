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
    public class GetAllMakerSpacesRequestHandlerTests : IClassFixture<RequestHandlerFixture>
    {
        private RequestHandlerFixture RequestHandlerFixture;

        public GetAllMakerSpacesRequestHandlerTests(RequestHandlerFixture requestHandlerFixture)
        {
            RequestHandlerFixture = requestHandlerFixture;
        }

        [Fact]
        public async Task GetAllTest()
        {
            //Arrange
            var makerSpaces = RequestHandlerFixture.Fixture.Build<MakerSpace>().Without(x => x.Tools).CreateMany();

            RequestHandlerFixture.DbContext.MakerSpace.AddRange(makerSpaces);
            await RequestHandlerFixture.DbContext.SaveChangesAsync();

            var handler = new GetAllMakerSpacesRequestHandler(RequestHandlerFixture.DbContext);

            //Act
            var result = await handler.Handle(new GetAllMakerSpaces());

            //Assert
            result.Should().HaveCount(makerSpaces.Count());
        }
    }
}
