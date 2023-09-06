namespace PrinzipMonitorService.BLL.Models
{
    public class User
    {
        public string Email { get; set; }
        public List<Flat> Subscriptions { get; set; }
    }
}
