using Microsoft.EntityFrameworkCore;
using ResidentWebApp.Data;
using ResidentWebApp.Models;
using System.Threading.Tasks;

namespace ResidentWebApp.Repositories
{
    public class NewsEventRepository : INewsEventRepository
    {
        private readonly AppDbContext _context;

        public NewsEventRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<NewsEvents>> GetAllNewsEventsAsync()
        {
           return await _context.NewsEvents
                                 .OrderByDescending(ne => ne.Date) // Sort by Date in descending order
                                 .ToListAsync();
        }


    }
}