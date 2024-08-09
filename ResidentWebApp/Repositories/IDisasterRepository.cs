using ResidentWebApp.Data;
using ResidentWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResidentWebApp.Repositories
{
    public interface IDisasterRepository
    {
       public IEnumerable<Disaster> GetAllDisasters();
    }
}
