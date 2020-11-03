using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Queries.V1;
using MakersOfDenmark.Domain.Models;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.Application.Tests.Handlers
{
    public class SearchForMakerSpaceHandlerTest: IClassFixture<RequestFixture>
    {
        private readonly RequestFixture _requestFixture;
        public SearchForMakerSpaceHandlerTest(RequestFixture requestFixture)
        {
            _requestFixture = requestFixture;
        }

        [Fact]
        public async Task SearchForOneMakerSpaceTest()
        {
            //Configuration
            _requestFixture.FixtureRecursionConfiguration();

            //Arrange
            var makerSpaceOne = _requestFixture.Fixture.Build<MakerSpace>().Create();
            var makerSpaceTwo = _requestFixture.Fixture.Build<MakerSpace>().Create();

            _requestFixture.DbContext.MakerSpace.Add(makerSpaceOne);
            _requestFixture.DbContext.MakerSpace.Add(makerSpaceTwo);
            await _requestFixture.DbContext.SaveChangesAsync();

            //Act
            var handler = new SearchForMakerSpaceHandler(_requestFixture.DbContext);
            var result = await handler.Handle(new SearchForMakerSpace(makerSpaceOne.Name));

            //Assert
            result.ForEach(x => x.Name.Should().Be(makerSpaceOne.Name));
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task SearchForManyMakerSpacesTest()
        {
            //Configuration
            _requestFixture.FixtureRecursionConfiguration();

            //Arrange
            var makerSpaceOne = _requestFixture.Fixture.Build<MakerSpace>().With(x => x.Name, "Aarhus Universitet").Create();
            var makerSpaceTwo = _requestFixture.Fixture.Build<MakerSpace>().With(x => x.Name, "Aarhus Universitet").Create();
            var makerSpaceThree = _requestFixture.Fixture.Build<MakerSpace>().With(x => x.Name, "Randers Statsskole").Create();

            _requestFixture.DbContext.MakerSpace.Add(makerSpaceOne);
            _requestFixture.DbContext.MakerSpace.Add(makerSpaceTwo);
            _requestFixture.DbContext.MakerSpace.Add(makerSpaceThree);
            await _requestFixture.DbContext.SaveChangesAsync();

            //Act
            var handler = new SearchForMakerSpaceHandler(_requestFixture.DbContext);
            var result = await handler.Handle(new SearchForMakerSpace("Universitet"));

            //Assert
            result.ForEach(x => x.Name.Should().Contain("Universitet"));
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task SearchForMakerSpace_NotFoundTest()
        {
            //Arrange
            var makerSpace = _requestFixture.Fixture.Build<MakerSpace>().Without(x => x.Tools).Create();

            _requestFixture.DbContext.MakerSpace.Add(makerSpace);
            await _requestFixture.DbContext.SaveChangesAsync();

            //Act
            var handler = new SearchForMakerSpaceHandler(_requestFixture.DbContext);
            var result = await handler.Handle(new SearchForMakerSpace("NameDoesntExist"));

            //Assert
            result.Should().BeEmpty();
        }
    }
}
