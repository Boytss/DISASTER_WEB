using ResidentWebApp.Data;
using ResidentWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
public interface INewsEventRepository
{
    Task<IEnumerable<NewsEvents>> GetAllNewsEventsAsync();
}
