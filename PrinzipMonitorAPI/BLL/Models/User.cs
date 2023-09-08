using System.Collections;

namespace PrinzipMonitorService.BLL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<Flat> Flats { get; set; }

        public User()
        {
            Flats = new List<Flat>();
        }
    }
}
