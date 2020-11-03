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

namespace MakersOfDenmark.WebAPI.Tests
{
    public class EditMakerSpaceOrganizationHandlerTest : IClassFixture<RequestHandlerFixture>
    {
        private readonly RequestHandlerFixture _requestHandlerFixture;

        public EditMakerSpaceOrganizationHandlerTest(RequestHandlerFixture requestHandlerFixture)
        {
            _requestHandlerFixture = requestHandlerFixture;
        }
        [Fact]
        public async Task EditMakerSpaceOrganizationTest_ValuesAreDifferent()
        {
            //Configuration
            _requestHandlerFixture.FixtureRecursionConfiguration();

            //Arrange
            var testMakerSpace = _requestHandlerFixture.Fixture.Build<MakerSpace>()
                .Without(x => x.Id)
                .Create();
            _requestHandlerFixture.DbContext.MakerSpace.Add(testMakerSpace);
            _requestHandlerFixture.DbContext.SaveChanges();

            var request = _requestHandlerFixture.Fixture.Build<EditMakerSpaceOrganization>().With(x => x.MakerSpaceId, testMakerSpace.Id).Create();

            //Act
            var handler = new EditMakerSpaceOrganizationHandler(_requestHandlerFixture.DbContext);
            await handler.Handle(request);

            var postTestMakerSpace = _requestHandlerFixture.DbContext.MakerSpace.Include(x => x.Organization).ThenInclude(x=>x.Address).FirstOrDefault(x => x.Id == testMakerSpace.Id);

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
            var request = _requestHandlerFixture.Fixture.Build<EditMakerSpaceOrganization>().With(x => x.MakerSpaceId, randomId).Create();

            //Act
            var handler = new EditMakerSpaceOrganizationHandler(_requestHandlerFixture.DbContext);
            Func<Task> act = async () => await handler.Handle(request);

            //Assert
            await act.Should().ThrowAsync<NullReferenceException>();
        }
    }
}
