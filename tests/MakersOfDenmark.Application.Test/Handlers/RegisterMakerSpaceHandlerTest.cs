using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Commands.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.Application.Tests.Handlers
{
    public class RegisterMakerSpaceHandlerTest : IClassFixture<RequestFixture>
    {
        private readonly RequestFixture _requestFixture;

        public RegisterMakerSpaceHandlerTest(RequestFixture requestFixture)
        {
            _requestFixture = requestFixture;
        }
        [Fact]
        public async Task RegisterMakerSpaceTest()
        {
            //Arrange
            var request = _requestFixture.Fixture.Build<RegisterMakerSpace>()
                .With(x => x.LogoUrl, "https://localhost")
                .With(x => x.AccessType, Domain.Enums.AccessType.Public)
                .Create();

            //Act
            var handler = new RegisterMakerSpaceHandler(_requestFixture.DbContext);

            //Assert
            _requestFixture.DbContext.MakerSpace.Should().HaveCount(0);
            var creater = await handler.Handle(request);

            creater.Should().NotBeEmpty();
            _requestFixture.DbContext.MakerSpace.Should().HaveCount(1);

        }
    }
}
