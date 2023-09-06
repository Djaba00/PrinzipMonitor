using System.Security.Cryptography.Xml;

namespace PrinzipMonitorService.BLL.Models
{
    public class Flat
    {
        public string Url { get; set; }
        public decimal LastPrice { get; set; }
        public List<User> Observers { get; set; }
    }
}
