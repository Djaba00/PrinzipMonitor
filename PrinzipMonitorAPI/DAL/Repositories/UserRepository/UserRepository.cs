using Microsoft.EntityFrameworkCore;
using PrinzipMonitorService.BLL.Models;
using PrinzipMonitorService.DAL.ApplicationContext.MsSql;

namespace PrinzipMonitorService.DAL.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        MsSqlDbContext _db;
        public UserRepository(MsSqlDbContext db) 
        {
            _db = db;
        }

        public void AddSubscription(string email, string url)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                user = new User() { Email = email };
            }

            var flat = _db.Flats.FirstOrDefault(f => f.Url == url);

            if (flat == null)
            {
                flat = new Flat() { Url = url };
            }

            user.Subscriptions.Add(flat);
            flat.Observers.Add(user);

            _db.Users.Add(user);
        }

        public List<Flat> GetSubscriptionsByEmail(string email)
        {
            var results = _db.Users.FirstOrDefault(u => u.Email == email).Subscriptions;

            return results;
        }
    }
}
