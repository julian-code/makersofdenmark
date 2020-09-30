using MakersOfDenmark.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakersOfDenmark.Infrastructure.Persistence.Extensions
{
    public static class Extensions
    {
        public static async Task<List<MakerSpace>> ListAsync (this IQueryable<MakerSpace> makerSpaces) 
        {
            return await makerSpaces.ToListAsync();
        }
    }
}
