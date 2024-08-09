using Microsoft.EntityFrameworkCore;
using ResidentWebApp.Data;
using ResidentWebApp.Models;
using System.Threading.Tasks;

namespace ResidentWebApp.Repositories
{
    public class TutorialTextRepository : ITutorialTextRepository
    {
        private readonly AppDbContext _context;

        public TutorialTextRepository(AppDbContext context)
        {
            _context = context;
        }

        public Tip GetTutorialTextByDisasterName(string disasterName)
        {
            var disaster = _context.Disasters
        .FirstOrDefault(d => d.DisasterName.ToLower() == disasterName.ToLower());

    if (disaster != null)
    {
        return _context.TutorialTexts
            .FirstOrDefault(tt => tt.DisasterID == disaster.DisasterID);
    }

    return null;
        }
    }
}