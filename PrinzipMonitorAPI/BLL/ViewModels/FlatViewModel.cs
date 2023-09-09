using System;
namespace PrinzipMonitorService.BLL.ViewModels
{
	public class FlatViewModel
	{
		public int Id { get; set; }
		public string Url { get; set; }
		public List<string> Observers { get; set; }
	}
}

