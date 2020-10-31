using AutoFixture;
using MakersOfDenmark.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace MakersOfDenmark.Application.Tests.Validator
{
    public class ValidatorFixture : IDisposable
    {
        private readonly DbContextOptions<MODContext> _options;
        public MODContext DbContext { get; private set; }
        public Fixture Fixture { get; private set; }

        public ValidatorFixture()
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
    }
}
