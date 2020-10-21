using AutoFixture;
using FluentAssertions;
using MakersOfDenmark.Application.Commands.V1.admin;
using MakersOfDenmark.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.WebAPI.Tests
{
    public class AddMakerSpaceToolHandlerTest : IClassFixture<RequestHandlerFixture>
    {
        private readonly RequestHandlerFixture _requestHandlerFixture;

        public AddMakerSpaceToolHandlerTest(RequestHandlerFixture requestHandlerFixture)
        {
            _requestHandlerFixture = requestHandlerFixture;
        }

        [Fact]
        public async Task AddToolToMakerSpaceTest()
        {
            _requestHandlerFixture.Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _requestHandlerFixture.Fixture.Behaviors.Remove(b));
            _requestHandlerFixture.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var makerSpace = _requestHandlerFixture.Fixture.Build<MakerSpace>().Without(x => x.Tools).With(x => x.Address, new Address("Test Street", "Test City", "Test Country", "Test Postcode")).Create();
            _requestHandlerFixture.DbContext.MakerSpace.Add(makerSpace);
            _requestHandlerFixture.DbContext.SaveChangesAsync();

            var handler = new AddMakerSpaceToolsHandler(_requestHandlerFixture.DbContext);
            var req = _requestHandlerFixture.Fixture.Build<AddMakerSpaceTool>().With(x => x.MakerSpaceId, makerSpace.Id).Create();

            makerSpace.Tools.Should().HaveCount(0);

            handler.Handle(req);

            makerSpace.Tools.Should().HaveCount(1);

        }

    }
}
