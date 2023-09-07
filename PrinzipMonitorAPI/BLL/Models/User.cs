namespace PrinzipMonitorService.BLL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<Flat> Subscriptions { get; set; } = new List<Flat>();

        //public User()
        //{
        //    Subscriptions = new List<Flat>();
        //}
    }
}
