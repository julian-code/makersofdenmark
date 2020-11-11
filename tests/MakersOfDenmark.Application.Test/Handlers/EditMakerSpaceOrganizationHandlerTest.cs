using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Commands.V1.admin;
using MakersOfDenmark.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.Application.Tests.Handlers
{
    public class EditMakerSpaceOrganizationHandlerTest : IClassFixture<RequestFixture>
    {
        private readonly RequestFixture _requestFixture;

        public EditMakerSpaceOrganizationHandlerTest(RequestFixture requestFixture)
        {
            _requestFixture = requestFixture;
        }
        [Fact]
        public async Task EditMakerSpaceOrganizationTest_ValuesAreDifferent()
        {
            //Configuration
            _requestFixture.FixtureRecursionConfiguration();

            //Arrange
            var testMakerSpace = _requestFixture.Fixture.Build<MakerSpace>()
                .Without(x => x.Id)
                .Create();
            _requestFixture.DbContext.MakerSpace.Add(testMakerSpace);
            _requestFixture.DbContext.SaveChanges();

            var request = _requestFixture.Fixture.Build<EditMakerSpaceOrganization>().With(x => x.MakerSpaceId, testMakerSpace.Id).Create();

            //Act
            var handler = new EditMakerSpaceOrganizationHandler(_requestFixture.DbContext);
            await handler.Handle(request);

            var postTestMakerSpace = _requestFixture.DbContext.MakerSpace.Include(x => x.Organization).ThenInclude(x=>x.Address).FirstOrDefault(x => x.Id == testMakerSpace.Id);

            //Assert
            postTestMakerSpace.Organization.Name.Should().Be(request.OrganizationName);
            postTestMakerSpace.Organization.OrganizationType.Should().Be(request.OrganizationType);
            postTestMakerSpace.Organization.Address.Street.Should().Be(request.Street);
            postTestMakerSpace.Organization.Address.City.Should().Be(request.City);
            postTestMakerSpace.Organization.Address.PostCode.Should().Be(request.PostCode);
            postTestMakerSpace.Organization.Address.Country.Should().Be(request.Country);
        }
        [Fact]
        public async Task EditMakerSpaceContactInfo_ThrowsExceptionWhenMakerSpaceCantBeFound()
        {
            //Arrange
            var randomId = Guid.NewGuid();
            var request = _requestFixture.Fixture.Build<EditMakerSpaceOrganization>().With(x => x.MakerSpaceId, randomId).Create();

            //Act
            var handler = new EditMakerSpaceOrganizationHandler(_requestFixture.DbContext);
            Func<Task> act = async () => await handler.Handle(request);

            //Assert
            await act.Should().ThrowAsync<NullReferenceException>();
        }
    }
}
