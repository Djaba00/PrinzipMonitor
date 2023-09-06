namespace PrinzipMonitorService.DAL.Repositories.FlatRepository
{
    public interface IFlatRepository
    {
        void CheckPrice(string url);
        void HandleObservers();
    }
}
