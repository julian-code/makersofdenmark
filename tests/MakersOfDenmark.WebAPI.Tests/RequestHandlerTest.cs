using AutoFixture;
using MakersOfDenmark.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MakersOfDenmark.WebAPI.Tests
{
    public abstract class RequestHandlerTest
    {
        protected readonly DbContextOptions<MODContext> _options;
        protected readonly MODContext _dbContext;
        protected readonly Fixture _fixture;

        protected RequestHandlerTest()
        {
            _options = new DbContextOptionsBuilder<MODContext>()
                .UseInMemoryDatabase(databaseName: "MakersOfDenmarkDatabase")
                .Options;

            _dbContext = new MODContext(_options);

            _fixture = new Fixture();
        }
    }
}
