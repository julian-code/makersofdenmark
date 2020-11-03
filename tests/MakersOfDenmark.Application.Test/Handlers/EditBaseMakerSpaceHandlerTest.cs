using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Commands.V1.admin;
using MakersOfDenmark.Domain.Enums;
using MakersOfDenmark.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.Application.Tests.Handlers
{
    public class EditBaseMakerSpaceHandlerTest : IClassFixture<RequestFixture>
    {
        private readonly RequestFixture _requestFixture;

        public EditBaseMakerSpaceHandlerTest(RequestFixture requestFixture)
        {
            _requestFixture = requestFixture;
        }

        [Fact]
        public async Task EditMakerSpaceTest()
        {
            _requestFixture.FixtureRecursionConfiguration();

            var testMakerSpace = _requestFixture.Fixture.Build<MakerSpace>().Without(x => x.Id).With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode")).Create();
            _requestFixture.DbContext.MakerSpace.Add(testMakerSpace);
            _requestFixture.DbContext.SaveChanges();

            var request = _requestFixture.Fixture.Build<EditBaseMakerSpace>().With(x => x.MakerSpaceId, testMakerSpace.Id).With(x => x.LogoUrl, "https://google.com").With(x => x.AccessType, AccessType.Private).Create();
            var handler = new EditBaseMakerSpaceHandler(_requestFixture.DbContext);
            await handler.Handle(request);

            var postTestMakerSpace = _requestFixture.DbContext.MakerSpace.FirstOrDefault(x => x.Id == testMakerSpace.Id);

            postTestMakerSpace.Name.Should().Be(request.Name);
            postTestMakerSpace.VATNumber.Should().Be(request.VATNumber);
            postTestMakerSpace.Logo.Should().Be(request.LogoUrl);
            postTestMakerSpace.AccessType.Should().Be(request.AccessType);
        }
        [Fact]
        public async Task EditMakerSpaceTest_ValuesAreDifferent()
        {
            _requestFixture.FixtureRecursionConfiguration();

            var name = "test MakerSpace";
            var vatNumber = "Boring VATNumber 123";
            var accessType = AccessType.Public;
            var logoUrl = new Uri("https://localhost/picture.jpg");
            var testMakerSpace = _requestFixture.Fixture.Build<MakerSpace>()
                .Without(x => x.Id)
                .With(x => x.Name, name).With(x => x.VATNumber, vatNumber).With(x => x.Logo, logoUrl).With(x => x.AccessType, accessType)
                .With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode"))
                .Create();
            _requestFixture.DbContext.MakerSpace.Add(testMakerSpace);
            _requestFixture.DbContext.SaveChanges();

            var request = _requestFixture.Fixture.Build<EditBaseMakerSpace>().With(x=> x.MakerSpaceId , testMakerSpace.Id).With(x=>x.LogoUrl, "https://localhost").With(x=> x.AccessType, AccessType.Private).Create();
            var handler = new EditBaseMakerSpaceHandler(_requestFixture.DbContext);
            await handler.Handle(request);

            var postTestMakerSpace = _requestFixture.DbContext.MakerSpace.FirstOrDefault(x => x.Id == testMakerSpace.Id);

            postTestMakerSpace.Name.Should().NotBe(name);
            postTestMakerSpace.VATNumber.Should().NotBe(vatNumber);
            postTestMakerSpace.Logo.Should().NotBe(logoUrl);
            postTestMakerSpace.AccessType.Should().NotBe(accessType);
        }
        [Fact]
        public void EditMakerSpaceTest_ThrowsExceptionWhenMakerSpaceCantBeFound()
        {
            var randomId = Guid.NewGuid();
            var handler = new EditBaseMakerSpaceHandler(_requestFixture.DbContext);
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
