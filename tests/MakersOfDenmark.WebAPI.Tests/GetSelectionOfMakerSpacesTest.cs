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
    public class GetSelectionOfMakerSpacesTest: IClassFixture<RequestHandlerFixture>
    {
        private RequestHandlerFixture _requestHandlerFixture;
        public GetSelectionOfMakerSpacesTest(RequestHandlerFixture requestHandlerFixture)
        {
            _requestHandlerFixture = requestHandlerFixture;
        }

        [Fact]
        public async Task GetSelectionTest()
        {
            //Arrange
            var makerSpace = _requestHandlerFixture.Fixture.Build<MakerSpace>().Without(x => x.ContactInfo).Without(x => x.VATNumber).Without(x => x.Organization).Without(x => x.Tools).Create();

            _requestHandlerFixture.DbContext.MakerSpace.Add(makerSpace);
            await _requestHandlerFixture.DbContext.SaveChangesAsync();

            //Act
            var handler = new GetSelectionOfMakerSpacesHandler(_requestHandlerFixture.DbContext);
            var result = await handler.Handle(new GetSelectionOfMakerSpaces(makerSpace.Name));

            //Assert
            result.Name.Should().Be(makerSpace.Name);
        }
    }
}
