using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoLocApi.Models
{
    public class GeoLocApiContext : DbContext, IGeoLocApiContext
    {
        public GeoLocApiContext (DbContextOptions<GeoLocApiContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<GeoLocation> GeoLocations { get; set; }

        public Task<List<GeoLocation>> ListAsync()
        {
            return GeoLocations.AsNoTracking().ToListAsync();
        }

        public Task<bool> AlreadyExist(string ip)
        {
            foreach(var obj in GeoLocations)
            {
                if (obj.Ip == ip)
                    return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
