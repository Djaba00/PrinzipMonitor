using System.Security.Cryptography.Xml;

namespace PrinzipMonitorService.BLL.Models
{
    public class Flat
    {
        public string Refer { get; set; }
        public decimal ActualPrice { get; set; }
        public List<decimal> PriceHistory { get; set; }
        public List<User> Observers { get; set; }
    }
}
