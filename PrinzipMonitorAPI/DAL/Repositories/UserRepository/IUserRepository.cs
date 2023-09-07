using PrinzipMonitorService.BLL.Models;

namespace PrinzipMonitorService.DAL.Repositories.UserRepository
{
    public interface IUserRepository
    {
        void Create(User user);
        User Get(string email);
        IEnumerable<User> GetAll();
        void Update(User updateUser);
    }
}
