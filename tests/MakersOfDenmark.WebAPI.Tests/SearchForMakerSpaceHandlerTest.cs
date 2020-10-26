using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Queries.V1;
using MakersOfDenmark.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.WebAPI.Tests
{
    public class SearchForMakerSpaceHandlerTest: IClassFixture<RequestHandlerFixture>
    {
        private RequestHandlerFixture _requestHandlerFixture;
        public SearchForMakerSpaceHandlerTest(RequestHandlerFixture requestHandlerFixture)
        {
            _requestHandlerFixture = requestHandlerFixture;
        }

        [Fact]
        public async Task SearchForOneMakerSpaceTest()
        {
            //Arrange
            var makerSpace = _requestHandlerFixture.Fixture.Build<MakerSpace>().With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode")).Without(x => x.ContactInfo).Without(x => x.VATNumber).Without(x => x.Organization).Without(x => x.Tools).Create();

            _requestHandlerFixture.DbContext.MakerSpace.Add(makerSpace);
            await _requestHandlerFixture.DbContext.SaveChangesAsync();

            //Act
            var handler = new SearchForMakerSpaceHandler(_requestHandlerFixture.DbContext);
            var result = await handler.Handle(new SearchForMakerSpace(makerSpace.Name));

            //Assert
            result.ForEach(x => x.Name.Should().Be(makerSpace.Name));
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task SearchForManyMakerSpacesTest()
        {
            //Arrange
            var makerSpaceOne = _requestHandlerFixture.Fixture.Build<MakerSpace>().With(x => x.Name, () => "Aarhus Universitet").With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode")).Without(x => x.ContactInfo).Without(x => x.VATNumber).Without(x => x.Organization).Without(x => x.Tools).Create();
            var makerSpaceTwo = _requestHandlerFixture.Fixture.Build<MakerSpace>().With(x => x.Name, () => "Aalborg Universitet").With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode")).Without(x => x.ContactInfo).Without(x => x.VATNumber).Without(x => x.Organization).Without(x => x.Tools).Create();

            _requestHandlerFixture.DbContext.MakerSpace.Add(makerSpaceOne);
            _requestHandlerFixture.DbContext.MakerSpace.Add(makerSpaceTwo);
            await _requestHandlerFixture.DbContext.SaveChangesAsync();

            //Act
            var handler = new SearchForMakerSpaceHandler(_requestHandlerFixture.DbContext);
            var result = await handler.Handle(new SearchForMakerSpace("Universitet"));

            //Assert
            result.ForEach(x => x.Name.Should().Contain("Universitet"));
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task SearchForMakerSpace_NotFoundTest()
        {
            //Arrange
            var makerSpace = _requestHandlerFixture.Fixture.Build<MakerSpace>().Without(x => x.Tools).Create();

            _requestHandlerFixture.DbContext.MakerSpace.Add(makerSpace);
            await _requestHandlerFixture.DbContext.SaveChangesAsync();

            //Act
            var handler = new SearchForMakerSpaceHandler(_requestHandlerFixture.DbContext);
            var result = await handler.Handle(new SearchForMakerSpace("NameDoesntExist"));

            //Assert
            Assert.Null(result);
        }
    }
}
