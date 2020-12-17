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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
