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
    public class EditMakerSpaceContactInfoHandlerTest : IClassFixture<RequestHandlerFixture>
    {
        private readonly RequestHandlerFixture _requestHandlerFixture;

        public EditMakerSpaceContactInfoHandlerTest(RequestHandlerFixture requestHandlerFixture)
        {
            _requestHandlerFixture = requestHandlerFixture;
        }
        [Fact]
        public async Task EditMakerSpaceContactInfoTest_ValuesAreDifferent()
        {
            _requestHandlerFixture.FixtureRecursionConfiguration();

            var newContactInfo = _requestHandlerFixture.Fixture.Build<ContactInfo>().Create();
            var testMakerSpace = _requestHandlerFixture.Fixture.Build<MakerSpace>()
                .Without(x => x.Id)
                .With(x => x.ContactInfo, newContactInfo)
                .Create();
            _requestHandlerFixture.DbContext.MakerSpace.Add(testMakerSpace);
            _requestHandlerFixture.DbContext.SaveChanges();

            var request = _requestHandlerFixture.Fixture.Build<EditMakerSpaceContactInfo>().With(x => x.MakerSpaceId, testMakerSpace.Id).Create();
            var handler = new EditMakerSpaceContactInfoHandler(_requestHandlerFixture.DbContext);
            await handler.Handle(request);

            var postTestMakerSpace = _requestHandlerFixture.DbContext.MakerSpace.Include(x => x.ContactInfo).FirstOrDefault(x => x.Id == testMakerSpace.Id);

            postTestMakerSpace.ContactInfo.Email.Should().NotBe(newContactInfo.Email);
            postTestMakerSpace.ContactInfo.Phone.Should().NotBe(newContactInfo.Phone);
        }
        [Fact]
        public async Task EditMakerSpaceContactInfo_ThrowsExceptionWhenMakerSpaceCantBeFound()
        {
            var randomId = Guid.NewGuid();
            var handler = new EditMakerSpaceContactInfoHandler(_requestHandlerFixture.DbContext);
            var request = _requestHandlerFixture.Fixture.Build<EditMakerSpaceContactInfo>().With(x => x.MakerSpaceId, randomId).Create();
            Func<Task> act = async () => await handler.Handle(request);
            await act.Should().ThrowAsync<NullReferenceException>();
        }
    }
}
