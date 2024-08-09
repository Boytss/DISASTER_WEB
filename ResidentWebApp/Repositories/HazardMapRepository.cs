using Microsoft.EntityFrameworkCore;
using ResidentWebApp.Data;
using ResidentWebApp.Models;
using System.Threading.Tasks;

namespace ResidentWebApp.Repositories
{
    public class HazardMapRepository : IHazardMapRepository
    {
        private readonly AppDbContext _context;

        public HazardMapRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HazardMap>> GetAllHazardMapsAsync()
        {
             return await _context.HazardMaps.ToListAsync();
        }

    }
}