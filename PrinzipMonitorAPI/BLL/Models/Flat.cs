using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.Xml;

namespace PrinzipMonitorService.BLL.Models
{
    public class Flat
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? LastPrice { get; set; }
        public List<User> Observers { get; set; } = new List<User>();

        //public Flat() 
        //{ 
        //    Observers = new List<User>();
        //}
    }
}
