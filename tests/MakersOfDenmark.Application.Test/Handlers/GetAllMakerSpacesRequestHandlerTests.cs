using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Queries.V1;
using MakersOfDenmark.Domain.Models;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static MakersOfDenmark.Application.Queries.V1.MakerSpaceViewmodel;

namespace MakersOfDenmark.Application.Tests.Handlers
{
    public class GetAllMakerSpacesRequestHandlerTests : IClassFixture<RequestFixture>
    {
        private readonly RequestFixture _requestFixture;

        public GetAllMakerSpacesRequestHandlerTests(RequestFixture requestFixture)
        {
            _requestFixture = requestFixture;
        }

        [Fact]
        public async Task GetAllTest()
        {
            //Arrange
            var makerSpaces = _requestFixture.Fixture.Build<MakerSpace>().Without(x => x.Tools).CreateMany();

            _requestFixture.DbContext.MakerSpace.AddRange(makerSpaces);
            await _requestFixture.DbContext.SaveChangesAsync();

            var handler = new GetAllMakerSpacesRequestHandler(_requestFixture.DbContext);

            //Act
            var result = await handler.Handle(new GetAllMakerSpaces());

            //Assert
            result.MakerSpaces.Should().HaveCount(makerSpaces.Count());
        }
        [Fact]
        public void ConvertAddressToViewmodel()
        {
            var address = _requestFixture.Fixture.Create<Address>();
            var addressVM = AddressViewmodel.Create(address);

            addressVM.Street.Should().Be(address.Street);
            addressVM.PostCode.Should().Be(address.PostCode);
            addressVM.City.Should().Be(address.City);
            addressVM.Country.Should().Be(address.Country);
        }
        [Fact]
        public void ConvertContactInfoToViewmodel()
        {
            var contactInfo = _requestFixture.Fixture.Create<ContactInfo>();
            var ciVM = ContactInformationViewModel.Create(contactInfo);

            ciVM.Email.Should().Be(contactInfo.Email);
            ciVM.Phone.Should().Be(contactInfo.Phone);
        }
    }
}
