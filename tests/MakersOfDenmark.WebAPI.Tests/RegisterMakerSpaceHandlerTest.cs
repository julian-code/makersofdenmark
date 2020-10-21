using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Commands.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.WebAPI.Tests
{
    public class RegisterMakerSpaceHandlerTest : IClassFixture<RequestHandlerFixture>
    {
        private readonly RequestHandlerFixture _requestHandlerFixture;

        public RegisterMakerSpaceHandlerTest(RequestHandlerFixture requestHandlerFixture)
        {
            _requestHandlerFixture = requestHandlerFixture;
        }
        [Fact]
        public async Task RegisterMakerSpaceTest()
        {
            var request = _requestHandlerFixture.Fixture.Build<RegisterMakerSpace>()
                .With(x => x.LogoUrl, "https://localhost")
                .With(x => x.AccessType, Domain.Enums.AccessType.Public)
                .Create();

            var handler = new RegisterMakerSpaceHandler(_requestHandlerFixture.DbContext);

            _requestHandlerFixture.DbContext.MakerSpace.Should().HaveCount(0);
            var creater = await handler.Handle(request);

            creater.Should().NotBeEmpty();
            _requestHandlerFixture.DbContext.MakerSpace.Should().HaveCount(1);

        }
    }
}
