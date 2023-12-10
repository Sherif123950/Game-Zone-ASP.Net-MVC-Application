using AutoMapper;
using DataAccessLayer.Entities;
using GameZone.ViewModels;

namespace GameZone.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<CreateGameFormViewmodel, Game>()
				.ForMember(dest => dest.Devices, opt => opt.MapFrom(src => src.SelectedDevices
				.Select(deviceId => new GameDevice { DeviceId = deviceId })))
				.ReverseMap();
			CreateMap<Game, EditFormViewModel>()
	            .ForMember(dest => dest.SelectedDevices, opt => opt.MapFrom(src => src.Devices.Select(device => device.DeviceId).ToList()))
	            .ForMember(dest => dest.Devices, opt => opt.Ignore()).ReverseMap()
				.ForMember(dest => dest.Devices, opt => opt.MapFrom(src => src.SelectedDevices.Select(deviceId => new GameDevice { DeviceId = deviceId }).ToList())); ;
             //			CreateMap<EditFormViewModel, Game>().ForMember(dest => dest.Devices, opt => opt.Ignore())
             //.ForMember(dest => dest.Devices, opt => opt.MapFrom(src => src.SelectedDevices.Select(deviceId => new GameDevice { DeviceId = deviceId }).ToList()));
			 //CreateMap<Game, EditFormViewModel>()
			 //.ForMember(dest => dest.SelectedDevices, opt => opt.MapFrom(src => src.Devices.Select(device => device.DeviceId).ToList()));
		}
	}
}
