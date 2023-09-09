using Microsoft.EntityFrameworkCore;
using PrinzipMonitorService.BLL.Models;
using PrinzipMonitorService.DAL.ApplicationContext.MsSql;
using System;
using System.Net;
using System.Net.Mail;

namespace PrinzipMonitorService.DAL.Repositories.FlatRepository
{
    public class FlatRepository : IFlatRepository
    {
        MsSqlDbContext _db;
        public FlatRepository(MsSqlDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Flat flat)
        {
            if ( _db.Flats.FirstOrDefault(f => f.Url == flat.Url) is null )
            {
                _db.Flats.Add(flat);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Flat> GetAsync(string url)
        {
            return await _db.Flats.Include(f => f.Users).FirstOrDefaultAsync(f => f.Url == url);
        }

        public async Task<List<Flat>> GetAllAsync()
        {
            return await _db.Flats.Include(f => f.Users).ToListAsync();
        }

        public async Task UpdateAsync(Flat updateFlat)
        {
            Flat currentFlat = await GetAsync(updateFlat.Url);

            currentFlat.LastPrice = updateFlat.LastPrice;
            currentFlat.Users = updateFlat.Users;

            _db.Flats.Update(currentFlat);
            await _db.SaveChangesAsync();
        }

        //public async Task<List<string>> GetUsersByFlat(string url)
        //{
        //    Flat currentFlat = await Get(url);


        //}
    }
}
