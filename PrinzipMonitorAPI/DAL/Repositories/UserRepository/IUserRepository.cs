using PrinzipMonitorService.BLL.Models;

namespace PrinzipMonitorService.DAL.Repositories.UserRepository
{
    public interface IUserRepository
    {
        void AddUser(string Email);
        IEnumerable<User> GetAll();
        User GetByEmail(string Email);
        List<Flat> GetSubscriptionByEmail(string Email);
    }
}
