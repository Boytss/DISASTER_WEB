using Microsoft.EntityFrameworkCore;
using ResidentWebApp.Data;
using ResidentWebApp.Models;
using System.Threading.Tasks;

namespace ResidentWebApp.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly AppDbContext _context;

        public VideoRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Video>> GetAllVideosAsync()
        {
            return await _context.Videos.ToListAsync();
        }


    }
}