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
            //Configuration
            _requestFixture.FixtureRecursionConfiguration();

            //Arrange
            var testMakerSpace = _requestFixture.Fixture.Build<MakerSpace>().Without(x => x.Id).With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode")).Create();
            _requestFixture.DbContext.MakerSpace.Add(testMakerSpace);
            _requestFixture.DbContext.SaveChanges();

            var request = _requestFixture.Fixture.Build<EditBaseMakerSpace>().With(x => x.MakerSpaceId, testMakerSpace.Id).With(x => x.LogoUrl, "https://google.com").With(x => x.AccessType, AccessType.Private).Create();

            //Act
            var handler = new EditBaseMakerSpaceHandler(_requestFixture.DbContext);
            await handler.Handle(request);
            var postTestMakerSpace = _requestFixture.DbContext.MakerSpace.FirstOrDefault(x => x.Id == testMakerSpace.Id);

            //Assert
            postTestMakerSpace.Name.Should().Be(request.Name);
            postTestMakerSpace.VATNumber.Should().Be(request.VATNumber);
            postTestMakerSpace.Logo.Should().Be(request.LogoUrl);
            postTestMakerSpace.AccessType.Should().Be(request.AccessType);
        }
        [Fact]
        public async Task EditMakerSpaceTest_ValuesAreDifferent()
        {
            //Configuration
            _requestFixture.FixtureRecursionConfiguration();

            //Arrange
            var name = "test MakerSpace";
            var vatNumber = "Boring VATNumber 123";
            var accessType = AccessType.Public;
            var logoUrl = "https://localhost/picture.jpg";
            var testMakerSpace = _requestFixture.Fixture.Build<MakerSpace>()
                .Without(x => x.Id)
                .With(x => x.Name, name).With(x => x.VATNumber, vatNumber).With(x => x.Logo, logoUrl).With(x => x.AccessType, accessType)
                .With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode"))
                .Create();
            _requestFixture.DbContext.MakerSpace.Add(testMakerSpace);
            _requestFixture.DbContext.SaveChanges();

            var request = _requestFixture.Fixture.Build<EditBaseMakerSpace>().With(x=> x.MakerSpaceId , testMakerSpace.Id).With(x=>x.LogoUrl, "https://localhost").With(x=> x.AccessType, AccessType.Private).Create();

            //Act
            var handler = new EditBaseMakerSpaceHandler(_requestFixture.DbContext);
            await handler.Handle(request);

            var postTestMakerSpace = _requestFixture.DbContext.MakerSpace.FirstOrDefault(x => x.Id == testMakerSpace.Id);

            //Assert
            postTestMakerSpace.Name.Should().NotBe(name);
            postTestMakerSpace.VATNumber.Should().NotBe(vatNumber);
            //postTestMakerSpace.Logo.Should().NotBe(logoUrl);
            postTestMakerSpace.AccessType.Should().NotBe(accessType);
        }
        [Fact]
        public void EditMakerSpaceTest_ThrowsExceptionWhenMakerSpaceCantBeFound()
        {
            //Arrange
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

            //Act
            Func<Task> act = async () => await handler.Handle(request);
            
            //Assert
            act.Should().Throw<NullReferenceException>();
        }
    }
}
