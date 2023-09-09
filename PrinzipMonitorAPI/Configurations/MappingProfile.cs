using System;
using AutoMapper;
using PrinzipMonitorService.BLL.Models;
using PrinzipMonitorService.BLL.ViewModels;

namespace PrinzipMonitorService.Configurations
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserViewModel>()
				.ForMember(x => x.Id, opt => opt.MapFrom(u => u.Id))
				.ForMember(x => x.Email, opt => opt.MapFrom(u => u.Email))
				.ForMember(x => x.Subscribtions, opt => opt.MapFrom(u => u.Flats.Select(f => f.Url)));

			CreateMap<Flat, FlatViewModel>()
				.ForMember(x => x.Id, opt => opt.MapFrom(f => f.Id))
				.ForMember(x => x.Url, opt => opt.MapFrom(f => f.Url))
				.ForMember(x => x.Observers, opt => opt.MapFrom(f => f.Users.Select(u => u.Email)));
        }
	}
}

