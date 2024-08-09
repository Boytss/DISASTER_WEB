using Microsoft.EntityFrameworkCore;
using ResidentWebApp.Data;
using ResidentWebApp.Models;
using System.Threading.Tasks;

namespace ResidentWebApp.Repositories
{
    public class DisasterRepository  : IDisasterRepository
    {
        private readonly AppDbContext _context;

        public DisasterRepository(AppDbContext context)
        {
            _context = context;
        }
         public IEnumerable<Disaster> GetAllDisasters()
        {
            return _context.Disasters.ToList();
        }

        public Tip GetTutorialTextByDisasterName(string disasterName)
        {
            var disasterId = _context.Disasters
                .Where(d => d.DisasterName == disasterName)
                .Select(d => d.DisasterID)
                .FirstOrDefault();

            if (disasterId != 0)
            {
                return _context.TutorialTexts
                    .FirstOrDefault(tt => tt.DisasterID == disasterId);
            }

            return null;
        }
    }
}