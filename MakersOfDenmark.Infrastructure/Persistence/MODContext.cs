using MakersOfDenmark.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MakersOfDenmark.Infrastructure.Persistence
{
    public class MODContext : DbContext
    {
        public MODContext(DbContextOptions<MODContext> options): base(options)
        {

        }
        public DbSet<MakerSpace> MakerSpace { get; set; }
    }
}
