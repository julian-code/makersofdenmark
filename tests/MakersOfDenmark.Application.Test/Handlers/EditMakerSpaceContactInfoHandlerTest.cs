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
    public class EditMakerSpaceContactInfoHandlerTest : IClassFixture<RequestFixture>
    {
        private readonly RequestFixture _requestFixture;

        public EditMakerSpaceContactInfoHandlerTest(RequestFixture requestFixture)
        {
            _requestFixture = requestFixture;
        }
        [Fact]
        public async Task EditMakerSpaceContactInfoTest_ValuesAreDifferent()
        {
            // Configuration
            _requestFixture.FixtureRecursionConfiguration();

            // Arrange
            var newContactInfo = _requestFixture.Fixture.Build<ContactInfo>().Create();
            var testMakerSpace = _requestFixture.Fixture.Build<MakerSpace>()
                .Without(x => x.Id)
                .With(x => x.ContactInfo, newContactInfo)
                .Create();
            _requestFixture.DbContext.MakerSpace.Add(testMakerSpace);
            _requestFixture.DbContext.SaveChanges();

            var request = _requestFixture.Fixture.Build<EditMakerSpaceContactInfo>().With(x => x.MakerSpaceId, testMakerSpace.Id).Create();
            
            // Act
            var handler = new EditMakerSpaceContactInfoHandler(_requestFixture.DbContext);
            await handler.Handle(request);

            var postTestMakerSpace = _requestFixture.DbContext.MakerSpace.Include(x => x.ContactInfo).FirstOrDefault(x => x.Id == testMakerSpace.Id);

            // Assert
            postTestMakerSpace.ContactInfo.Email.Should().NotBe(newContactInfo.Email);
            postTestMakerSpace.ContactInfo.Phone.Should().NotBe(newContactInfo.Phone);
        }
        [Fact]
        public async Task EditMakerSpaceContactInfo_ThrowsExceptionWhenMakerSpaceCantBeFound()
        {
            // Arrange
            var randomId = Guid.NewGuid();
            var handler = new EditMakerSpaceContactInfoHandler(_requestFixture.DbContext);
            
            // Act
            var request = _requestFixture.Fixture.Build<EditMakerSpaceContactInfo>().With(x => x.MakerSpaceId, randomId).Create();
            Func<Task> act = async () => await handler.Handle(request);
            
            // Assert
            await act.Should().ThrowAsync<NullReferenceException>();
        }
    }
}
