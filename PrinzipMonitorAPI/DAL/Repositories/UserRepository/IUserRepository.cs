using PrinzipMonitorService.BLL.Models;

namespace PrinzipMonitorService.DAL.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<User> GetAsync(string email);
        Task<List<User>> GetAllAsync();
        Task UpdateAsync(User updateUser);
    }
}
