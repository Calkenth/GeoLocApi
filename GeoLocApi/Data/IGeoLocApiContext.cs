using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoLocApi.Models
{
    public interface IGeoLocApiContext
    {
        DbSet<GeoLocation> GeoLocations { get; }
        Task<List<GeoLocation>> ListAsync();
        Task<bool >AlreadyExist(string ip);
    }
}