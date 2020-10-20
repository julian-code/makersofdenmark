﻿using AutoFixture;
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
    public class GetMakerSpaceByIdHandlerTests : IClassFixture<RequestHandlerFixture>
    {
        private readonly RequestHandlerFixture _requestHandlerFixture;

        public GetMakerSpaceByIdHandlerTests(RequestHandlerFixture requestHandlerFixture)
        {
            _requestHandlerFixture = requestHandlerFixture;
        }

        [Fact]
        public async Task GetMakerSpaceByIdTest()
        {
            _requestHandlerFixture.Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _requestHandlerFixture.Fixture.Behaviors.Remove(b));
            _requestHandlerFixture.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var actual = _requestHandlerFixture.Fixture.Build<MakerSpace>().With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode")).Create();

            _requestHandlerFixture.DbContext.MakerSpace.Add(actual);
            await _requestHandlerFixture.DbContext.SaveChangesAsync();

            var handler = new GetMakerSpaceByIdHandler(_requestHandlerFixture.DbContext);

            var result = await handler.Handle(new GetMakerSpaceById(actual.Id));

            result.Organization.Should().Be(actual.Organization.Name);
            result.Address.Should().Be(actual.Address.FullAddress);
            result.MakerSpaceType.Should().Be(actual.MakerSpaceType.Name);
            result.ContactInfo.Should().Contain(new string[] { actual.ContactInfo.Email, actual.ContactInfo.Phone });
            result.Logo.Should().Be(actual.Logo.ToString());
        }
    }
}