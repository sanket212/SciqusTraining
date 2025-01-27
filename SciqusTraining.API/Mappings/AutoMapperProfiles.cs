using AutoMapper;
using SciqusTraining.API.Models.Domains;
using SciqusTraining.API.Models.DTO;

namespace SciqusTraining.API.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionReqDto, Region>().ReverseMap();
            CreateMap<UpdateRegionReqDto, Region>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
            CreateMap<AddWalkReqDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<UpdateWalkReqDto, Walk>().ReverseMap();
            

        }

    }
}
