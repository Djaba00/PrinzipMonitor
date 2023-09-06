using Microsoft.EntityFrameworkCore;
using PrinzipMonitorService.BLL.Models;
using PrinzipMonitorService.DAL.ApplicationContext.MsSql;
using PrinzipMonitorService.DAL.Repositories.FlatRepository.Helpers;
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

        public void CheckPrice(string url)
        {
            var flat = _db.Flats.FirstOrDefault(f => f.Url == url);

            if (flat != null)
            {
                var newPrice = new PriceChecker(url).GetPrice(); 

                if (newPrice != flat.LastPrice)
                {
                    var oldPrice = flat.LastPrice;

                    flat.LastPrice = newPrice;

                    foreach (var user in flat.Observers)
                    {
                        NotifyObserver(user.Email, oldPrice, newPrice);
                    }
                }
            }
            else
            {
                // Exception("На данную квартиру нет подписок");
            }
        }

        public async Task NotifyObserver(string email, decimal oldPrice, decimal newPrice)
        {
            await EmailService.SendEmail(email, oldPrice, newPrice);
        }
    }
}
