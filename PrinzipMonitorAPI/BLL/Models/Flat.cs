using System.Collections;

namespace PrinzipMonitorService.BLL.Models
{
    public class Flat
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? LastPrice { get; set; }
        public List<User> Users { get; set; }

        public Flat()
        {
            Users = new List<User>();
        }
    }
}
