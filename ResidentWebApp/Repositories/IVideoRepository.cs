using ResidentWebApp.Data;
using ResidentWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
public interface IVideoRepository
{
    Task<IEnumerable<Video>> GetAllVideosAsync();
}
