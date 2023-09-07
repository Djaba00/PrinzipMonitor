using PrinzipMonitorService.BLL.Models;

namespace PrinzipMonitorService.DAL.Repositories.FlatRepository
{
    public interface IFlatRepository
    {
        void Create(Flat flat);
        Flat Get(string url);
        IEnumerable<Flat> GetAll();
        void Update(Flat updateFlat);
    }
}
