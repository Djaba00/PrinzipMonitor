using System.Security.Cryptography.Xml;

namespace PrinzipMonitorService.BLL.Models
{
    public class Flat
    {
        public string Refer { get; set; }
        public decimal Price { get; set; }
        public List<User> Observers { get; set; }
    }
}
