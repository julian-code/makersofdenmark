﻿using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Queries.V1;
using MakersOfDenmark.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var makerSpaceOne = _requestHandlerFixture.Fixture.Build<MakerSpace>().With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode")).Without(x => x.ContactInfo).Without(x => x.VATNumber).Without(x => x.Organization).Without(x => x.Tools).Create();
            var makerSpaceTwo = _requestHandlerFixture.Fixture.Build<MakerSpace>().With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode")).Without(x => x.ContactInfo).Without(x => x.VATNumber).Without(x => x.Organization).Without(x => x.Tools).Create();

            _requestHandlerFixture.DbContext.MakerSpace.Add(makerSpaceOne);
            _requestHandlerFixture.DbContext.MakerSpace.Add(makerSpaceTwo);
            await _requestHandlerFixture.DbContext.SaveChangesAsync();

            //Act
            var handler = new SearchForMakerSpaceHandler(_requestHandlerFixture.DbContext);
            var result = await handler.Handle(new SearchForMakerSpace(makerSpaceOne.Name));

            //Assert
            result.ForEach(x => x.Name.Should().Be(makerSpaceOne.Name));
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task SearchForManyMakerSpacesTest()
        {
            //Configuration
            _requestHandlerFixture.Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b
              => _requestHandlerFixture.Fixture.Behaviors.Remove(b));
             _requestHandlerFixture.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            //Arrange
            var makerSpaceOne = _requestHandlerFixture.Fixture.Build<MakerSpace>().With(x => x.Name, "Aarhus Universitet").Create();
            var makerSpaceTwo = _requestHandlerFixture.Fixture.Build<MakerSpace>().With(x => x.Name, "Aarhus Universitet").Create();
            var makerSpaceThree = _requestHandlerFixture.Fixture.Build<MakerSpace>().With(x => x.Name, "Randers Gymnasium").Create();

            _requestHandlerFixture.DbContext.MakerSpace.Add(makerSpaceOne);
            _requestHandlerFixture.DbContext.MakerSpace.Add(makerSpaceTwo);
            _requestHandlerFixture.DbContext.MakerSpace.Add(makerSpaceThree);
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
            result.Should().HaveCount(0);
        }
    }
}
