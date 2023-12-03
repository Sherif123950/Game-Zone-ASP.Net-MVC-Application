using AutoMapper;
using DataAccessLayer.Entities;
using GameZone.ViewModels;

namespace GameZone.Helpers
{
	public class MappingProfiles:Profile
	{
        public MappingProfiles()
        {
            CreateMap<CreateGameFormViewmodel,Game>()
                .ForMember(dest => dest.Devices, opt => opt.MapFrom(src => src.SelectedDevices
                .Select(deviceId => new GameDevice { DeviceId = deviceId })))
                .ReverseMap();
        }
    }
}
