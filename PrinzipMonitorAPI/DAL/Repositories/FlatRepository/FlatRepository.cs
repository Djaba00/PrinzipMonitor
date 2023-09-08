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

        public void Create(Flat flat)
        {
            if ( _db.Flats.FirstOrDefault(f => f.Url == flat.Url) is null )
            {
                _db.Flats.Add(flat);
                _db.SaveChanges();
            }
        }

        public Flat Get(string url)
        {
            return _db.Flats.Include(f => f.Users).FirstOrDefault(f => f.Url == url);
        }

        public IEnumerable<Flat> GetAll()
        {
            return _db.Flats.Include(f => f.Users);
        }

        public void Update(Flat updateFlat)
        {
            Flat currentFlat = Get(updateFlat.Url);

            currentFlat.LastPrice = updateFlat.LastPrice;
            currentFlat.Users = updateFlat.Users;

            _db.Flats.Update(currentFlat);
            _db.SaveChanges();
        }
    }
}
