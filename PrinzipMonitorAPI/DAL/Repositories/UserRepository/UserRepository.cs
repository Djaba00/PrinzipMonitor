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

        public void Create(User user)
        {
            if (_db.Users.FirstOrDefault(u => u.Email == user.Email) is null)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
            }
                
        }

        public User Get(string email)
        {
            return _db.Users.Include(u => u.Flats).FirstOrDefault(u => u.Email == email);
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users.Include(u => u.Flats);
        }

        public void Update(User updateUser)
        {
            User currentUser = Get(updateUser.Email);

            currentUser.Flats = updateUser.Flats;

            _db.Users.Update(currentUser);
            _db.SaveChanges();
        }
    }
}
