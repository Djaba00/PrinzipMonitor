namespace PrinzipMonitorService.BLL.Models
{
    public class User
    {
        public string Email { get; set; }
        public List<Flat> Subscriptions { get; set; }

        public User(string setEmail)
        {
            Email = setEmail;
            Subscriptions = new List<Flat>();
        }
    }
}
