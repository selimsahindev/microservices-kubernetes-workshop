using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            // Source -> Target
            CreateMap<Platform, Dtos.PlatformReadDTO>();
            CreateMap<PlatformCreateDTO, Platform>();
        }
    }
}