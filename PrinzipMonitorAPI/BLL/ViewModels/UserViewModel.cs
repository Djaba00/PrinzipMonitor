using System;
namespace PrinzipMonitorService.BLL.ViewModels
{
	public class UserViewModel
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public List<string> Subscribtions { get; set; }
	}
}

