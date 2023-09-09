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

        public async Task CreateAsync(User user)
        {
            if (_db.Users.FirstOrDefault(u => u.Email == user.Email) is null)
            {
                _db.Users.Add(user);
                await _db.SaveChangesAsync();
            }
                
        }

        public async Task<User> GetAsync(string email)
        {
            return await _db.Users.Include(u => u.Flats).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _db.Users.Include(u => u.Flats).ToListAsync();
        }

        public async Task UpdateAsync(User updateUser)
        {
            User currentUser = await GetAsync(updateUser.Email);

            currentUser.Flats = updateUser.Flats;

            _db.Users.Update(currentUser);
            await _db.SaveChangesAsync();
        }
    }
}
