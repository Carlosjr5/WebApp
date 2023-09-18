using AutoMapper;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        //Mapping Constructor ctor
        public AutoMapperProfiles()
         {
            CreateMap<Region, RegionDto>() .ReverseMap();

            CreateMap<AddRegionRequestDto, Region>().ReverseMap();

            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();


        }
    }













}
