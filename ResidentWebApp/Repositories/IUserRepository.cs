using ResidentWebApp.Models;
using System.Threading.Tasks;

namespace ResidentWebApp.Repositories
{
    public interface IUserRepository
    {
       Task<User?> GetUserByUsernameAsync(string username);
        Task<bool> UsernameExistsAsync(string username);
        Task<User> CreateUserAsync(User user);
    }
}