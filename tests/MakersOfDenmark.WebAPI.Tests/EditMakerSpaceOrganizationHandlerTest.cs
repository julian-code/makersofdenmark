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
            _requestHandlerFixture.Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _requestHandlerFixture.Fixture.Behaviors.Remove(b));
            _requestHandlerFixture.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());


            var testMakerSpace = _requestHandlerFixture.Fixture.Build<MakerSpace>()
                .Without(x => x.Id)
                .Create();
            _requestHandlerFixture.DbContext.MakerSpace.Add(testMakerSpace);
            _requestHandlerFixture.DbContext.SaveChanges();

            var request = _requestHandlerFixture.Fixture.Build<EditMakerSpaceOrganization>().With(x => x.Id, testMakerSpace.Id).Create();
            var handler = new EditMakerSpaceOrganizationHandler(_requestHandlerFixture.DbContext);
            await handler.Handle(request);

            var postTestMakerSpace = _requestHandlerFixture.DbContext.MakerSpace.Include(x => x.Organization).ThenInclude(x=>x.Address).FirstOrDefault(x => x.Id == testMakerSpace.Id);

            postTestMakerSpace.Organization.Name.Should().Be(request.OrganizationName);
            postTestMakerSpace.Organization.OrganizationType.Should().Be(request.OrganizationType);
            postTestMakerSpace.Organization.Address.Street.Should().Be(request.OrganizationStreet);
            postTestMakerSpace.Organization.Address.City.Should().Be(request.OrganizationCity);
            postTestMakerSpace.Organization.Address.PostCode.Should().Be(request.OrganizationPostCode);
            postTestMakerSpace.Organization.Address.Country.Should().Be(request.OrganizationCountry);
        }
        [Fact]
        public async Task EditMakerSpaceContactInfo_ThrowsExceptionWhenMakerSpaceCantBeFound()
        {
            var randomId = Guid.NewGuid();
            var handler = new EditMakerSpaceOrganizationHandler(_requestHandlerFixture.DbContext);
            var request = _requestHandlerFixture.Fixture.Build<EditMakerSpaceOrganization>().With(x => x.Id, randomId).Create();
            Func<Task> act = async () => await handler.Handle(request);
            await act.Should().ThrowAsync<NullReferenceException>();
        }
    }
}
