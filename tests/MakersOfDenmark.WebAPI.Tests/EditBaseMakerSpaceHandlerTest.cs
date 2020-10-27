using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Commands.V1.admin;
using MakersOfDenmark.Domain.Enums;
using MakersOfDenmark.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.WebAPI.Tests
{
    public class EditBaseMakerSpaceHandlerTest : IClassFixture<RequestHandlerFixture>
    {
        private readonly RequestHandlerFixture _requestHandlerFixture;

        public EditBaseMakerSpaceHandlerTest(RequestHandlerFixture requestHandlerFixture)
        {
            _requestHandlerFixture = requestHandlerFixture;
        }

        [Fact]
        public async Task EditMakerSpaceTest()
        {
            _requestHandlerFixture.FixtureRecursionConfiguration();

            var testMakerSpace = _requestHandlerFixture.Fixture.Build<MakerSpace>().Without(x => x.Id).With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode")).Create();
            _requestHandlerFixture.DbContext.MakerSpace.Add(testMakerSpace);
            _requestHandlerFixture.DbContext.SaveChanges();

            var request = _requestHandlerFixture.Fixture.Build<EditBaseMakerSpace>().With(x => x.MakerSpaceId, testMakerSpace.Id).With(x => x.LogoUrl, "https://google.com").With(x => x.AccessType, AccessType.Private).Create();
            var handler = new EditBaseMakerSpaceHandler(_requestHandlerFixture.DbContext);
            await handler.Handle(request);

            var postTestMakerSpace = _requestHandlerFixture.DbContext.MakerSpace.FirstOrDefault(x => x.Id == testMakerSpace.Id);

            postTestMakerSpace.Name.Should().Be(request.Name);
            postTestMakerSpace.VATNumber.Should().Be(request.VATNumber);
            postTestMakerSpace.Logo.Should().Be(request.LogoUrl);
            postTestMakerSpace.AccessType.Should().Be(request.AccessType);
        }
        [Fact]
        public async Task EditMakerSpaceTest_ValuesAreDifferent()
        {
            _requestHandlerFixture.FixtureRecursionConfiguration();

            var name = "test MakerSpace";
            var vatNumber = "Boring VATNumber 123";
            var accessType = AccessType.Public;
            var logoUrl = new Uri("https://localhost/picture.jpg");
            var testMakerSpace = _requestHandlerFixture.Fixture.Build<MakerSpace>()
                .Without(x => x.Id)
                .With(x => x.Name, name).With(x => x.VATNumber, vatNumber).With(x => x.Logo, logoUrl).With(x => x.AccessType, accessType)
                .With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode"))
                .Create();
            _requestHandlerFixture.DbContext.MakerSpace.Add(testMakerSpace);
            _requestHandlerFixture.DbContext.SaveChanges();

            var request = _requestHandlerFixture.Fixture.Build<EditBaseMakerSpace>().With(x=> x.MakerSpaceId , testMakerSpace.Id).With(x=>x.LogoUrl, "https://localhost").With(x=> x.AccessType, AccessType.Private).Create();
            var handler = new EditBaseMakerSpaceHandler(_requestHandlerFixture.DbContext);
            await handler.Handle(request);

            var postTestMakerSpace = _requestHandlerFixture.DbContext.MakerSpace.FirstOrDefault(x => x.Id == testMakerSpace.Id);

            postTestMakerSpace.Name.Should().NotBe(name);
            postTestMakerSpace.VATNumber.Should().NotBe(vatNumber);
            postTestMakerSpace.Logo.Should().NotBe(logoUrl);
            postTestMakerSpace.AccessType.Should().NotBe(accessType);
        }
        [Fact]
        public void EditMakerSpaceTest_ThrowsExceptionWhenMakerSpaceCantBeFound()
        {
            var randomId = Guid.NewGuid();
            var handler = new EditBaseMakerSpaceHandler(_requestHandlerFixture.DbContext);
            var request = new EditBaseMakerSpace
            {
                MakerSpaceId = randomId,
                Name = "Another and Different Name",
                VATNumber = "Another and Different VATNumber 904834",
                LogoUrl = "https://google.com/different",
                AccessType = AccessType.Private
            };
            Func<Task> act = async () => await handler.Handle(request);
            act.Should().Throw<NullReferenceException>();
        }
    }
}
