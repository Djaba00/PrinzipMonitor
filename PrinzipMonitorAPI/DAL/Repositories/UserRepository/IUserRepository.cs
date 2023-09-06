using PrinzipMonitorService.BLL.Models;

namespace PrinzipMonitorService.DAL.Repositories.UserRepository
{
    public interface IUserRepository
    {
        void AddSubscription(string email, string refer);
        List<Flat> GetSubscriptionsByEmail(string email);
    }
}
