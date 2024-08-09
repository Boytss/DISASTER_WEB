using ResidentWebApp.Data;
using ResidentWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResidentWebApp.Repositories
{
    public interface IEvacuationCenterRepository
    {
        Task<IEnumerable<EvacuationCenter>> GetAllAsync();
        Task<EvacuationCenter?> GetLatestCenterAsync();
        Task<IEnumerable<EvacuationRoom>> GetRoomsForEvacuationCenterAsync(int centerId);
        Task<EvacuationCenter?> GetCenterByIdAsync(int centerId);
    }
}
