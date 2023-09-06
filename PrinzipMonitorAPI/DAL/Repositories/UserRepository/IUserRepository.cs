using PrinzipMonitorService.BLL.Models;

namespace PrinzipMonitorService.DAL.Repositories.UserRepository
{
    public interface IUserRepository
    {
        void AddSubscription(string email, string url);
        List<Flat> GetSubscriptionsByEmail(string email);
    }
}
