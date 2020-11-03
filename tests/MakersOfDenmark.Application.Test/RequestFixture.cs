using AutoFixture;
using MakersOfDenmark.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace MakersOfDenmark.Application.Tests
{
    public class RequestFixture : IDisposable
    {
        private readonly DbContextOptions<MODContext> _options;
        public MODContext DbContext { get; private set; }
        public Fixture Fixture { get; private set; }

        public RequestFixture()
        {
            _options = new DbContextOptionsBuilder<MODContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            DbContext = new MODContext(_options);

            Fixture = new Fixture();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public void FixtureRecursionConfiguration() 
        {
            Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b
              => Fixture.Behaviors.Remove(b));
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
