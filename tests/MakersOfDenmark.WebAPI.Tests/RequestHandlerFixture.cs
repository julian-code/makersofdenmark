using AutoFixture;
using MakersOfDenmark.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace MakersOfDenmark.WebAPI.Tests
{
    public class RequestHandlerFixture : IDisposable
    {
        private readonly DbContextOptions<MODContext> _options;
        public MODContext DbContext { get; private set; }
        public Fixture Fixture { get; private set; }

        public RequestHandlerFixture()
        {
            _options = new DbContextOptionsBuilder<MODContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            DbContext = new MODContext(_options);

            Fixture = new Fixture();
            Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b
              => Fixture.Behaviors.Remove(b));
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
