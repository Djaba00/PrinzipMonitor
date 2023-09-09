using PrinzipMonitorService.BLL.Models;

namespace PrinzipMonitorService.DAL.Repositories.FlatRepository
{
    public interface IFlatRepository
    {
        Task CreateAsync(Flat flat);
        Task<Flat> GetAsync(string url);
        Task<List<Flat>> GetAllAsync();
        Task UpdateAsync(Flat updateFlat);
    }
}
