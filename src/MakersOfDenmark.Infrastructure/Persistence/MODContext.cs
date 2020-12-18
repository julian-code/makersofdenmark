using MakersOfDenmark.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MakersOfDenmark.Infrastructure.Persistence
{
    public class MODContext : DbContext
    {
        public MODContext(DbContextOptions<MODContext> options): base(options)
        {

        }
        public DbSet<MakerSpace> MakerSpace { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Badge> Badges { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
