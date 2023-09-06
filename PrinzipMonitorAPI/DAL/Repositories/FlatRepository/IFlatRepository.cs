using PrinzipMonitorService.BLL.Models;

namespace PrinzipMonitorService.DAL.Repositories.FlatRepository
{
    public interface IFlatRepository
    {
        void CheckPrice(string url);
        Task NotifyObserver(string email, decimal oldPrice, decimal newPrice);
    }
}
