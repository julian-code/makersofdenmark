using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Queries.V1;
using MakersOfDenmark.Domain.Models;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static MakersOfDenmark.Application.Queries.V1.MakerSpaceViewmodel;

namespace MakersOfDenmark.WebAPI.Tests
{
    public class GetAllMakerSpacesRequestHandlerTests : IClassFixture<RequestHandlerFixture>
    {
        private RequestHandlerFixture _requestHandlerFixture;

        public GetAllMakerSpacesRequestHandlerTests(RequestHandlerFixture requestHandlerFixture)
        {
            _requestHandlerFixture = requestHandlerFixture;
        }

        [Fact]
        public async Task GetAllTest()
        {
            //Arrange
            var makerSpaces = _requestHandlerFixture.Fixture.Build<MakerSpace>().Without(x => x.Tools).Without(x=>x.Followers).CreateMany();

            _requestHandlerFixture.DbContext.MakerSpace.AddRange(makerSpaces);
            await _requestHandlerFixture.DbContext.SaveChangesAsync();

            var handler = new GetAllMakerSpacesRequestHandler(_requestHandlerFixture.DbContext);

            //Act
            var result = await handler.Handle(new GetAllMakerSpaces());

            //Assert
            result.MakerSpaces.Should().HaveCount(makerSpaces.Count());
        }
        [Fact]
        public void ConvertAddressToViewmodel()
        {
            var address = _requestHandlerFixture.Fixture.Create<Address>();
            var addressVM = AddressViewmodel.Create(address);

            addressVM.Street.Should().Be(address.Street);
            addressVM.PostCode.Should().Be(address.PostCode);
            addressVM.City.Should().Be(address.City);
            addressVM.Country.Should().Be(address.Country);
        }
        [Fact]
        public void ConvertContactInfoToViewmodel()
        {
            var contactInfo = _requestHandlerFixture.Fixture.Create<ContactInfo>();
            var ciVM = ContactInformationViewModel.Create(contactInfo);

            ciVM.Email.Should().Be(contactInfo.Email);
            ciVM.Phone.Should().Be(contactInfo.Phone);
        }
    }
}
