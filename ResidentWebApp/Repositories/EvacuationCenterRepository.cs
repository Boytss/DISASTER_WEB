using Microsoft.EntityFrameworkCore;
using ResidentWebApp.Data;
using ResidentWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResidentWebApp.Repositories
{
    public class EvacuationCenterRepository : IEvacuationCenterRepository
    {
        private readonly AppDbContext _context;

        public EvacuationCenterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EvacuationCenter>> GetAllAsync()
        {
              return await _context.EvacuationCenters
                .OrderByDescending(c => c.Date)
                .ToListAsync();
        }

        public async Task<EvacuationCenter?> GetCenterByIdAsync(int centerId)
        {
              return await _context.EvacuationCenters
        .FirstOrDefaultAsync(c => c.CenterID == centerId);
        }

        public async Task<EvacuationCenter?> GetLatestCenterAsync()
        {
            return await _context.EvacuationCenters
                .OrderByDescending(c => c.Date)
                .FirstOrDefaultAsync();

        }
        public async Task<IEnumerable<EvacuationRoom>> GetRoomsForEvacuationCenterAsync(int centerId)
        {
            // Assuming you have an EvacuationRoom model and a DbSet<EvacuationRoom> in your AppDbContext
            return await _context.EvacuationRooms
                .Where(r => r.CenterID == centerId)
                .ToListAsync();
        }
    }
}
