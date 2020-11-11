using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Commands.V1.admin;
using MakersOfDenmark.Domain.Enums;
using MakersOfDenmark.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.Application.Tests.Handlers
{
    public class EditMakerSpaceAddressHandlerTest : IClassFixture<RequestFixture>
    {
        private readonly RequestFixture _requestFixture;

        public EditMakerSpaceAddressHandlerTest(RequestFixture requestFixture)
        {
            _requestFixture = requestFixture;
        }
        [Fact]
        public async Task EditMakerSpaceAddressTest_ValuesAreDifferent()
        {
            //Configuration
            _requestFixture.FixtureRecursionConfiguration();

            //Arrange
            var testAddress = new Address("Test Street", "Test City", "Test Country", "Test Postcode");
            var testMakerSpace = _requestFixture.Fixture.Build<MakerSpace>()
                .Without(x => x.Id)
                .With(x => x.AccessType, AccessType.Public)
                .With(x => x.Address, testAddress)
                .Create();
            _requestFixture.DbContext.MakerSpace.Add(testMakerSpace);
            _requestFixture.DbContext.SaveChanges();

            var request = _requestFixture.Fixture.Build<EditMakerSpaceAddress>().With(x => x.MakerSpaceId, testMakerSpace.Id).Create();
            
            //Act
            var handler = new EditMakerSpaceAddressHandler(_requestFixture.DbContext);
            await handler.Handle(request);

            var postTestMakerSpace = _requestFixture.DbContext.MakerSpace.Include(x => x.Address).FirstOrDefault(x => x.Id == testMakerSpace.Id);

            //Assert
            postTestMakerSpace.Address.Street.Should().NotBe(testAddress.Street);
            postTestMakerSpace.Address.City.Should().NotBe(testAddress.City);
            postTestMakerSpace.Address.Country.Should().NotBe(testAddress.Country);
            postTestMakerSpace.Address.PostCode.Should().NotBe(testAddress.PostCode);
        }
        [Fact]
        public async Task EditMakerSpaceAddress_ThrowsExceptionWhenMakerSpaceCantBeFound()
        {
            //Arrange
            var randomId = Guid.NewGuid();
            var request = _requestFixture.Fixture.Build<EditMakerSpaceAddress>().With(x => x.MakerSpaceId, randomId).Create();

            //Act
            var handler = new EditMakerSpaceAddressHandler(_requestFixture.DbContext);
            Func<Task> act = async () => await handler.Handle(request);

            //Assert
            await act.Should().ThrowAsync<NullReferenceException>();
        }
    }
}
