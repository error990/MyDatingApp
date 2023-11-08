using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers;
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // CreateMap<Source, Destination>();
        CreateMap<AppUser, MemberDto>()
            // -- Stack Overflow-Version (I use Records as Dtos) 
            // https://stackoverflow.com/questions/65382286/automapper-problem-with-mapping-records-type
            // Mapper needs default values in the records! Otherwise do it with classes
            .ForCtorParam(
                ctorParamName: "PhotoUrl", 
                m => m.MapFrom(src => src.Photos.FirstOrDefault(ph => ph.IsMain).Url)
            )
            .ForCtorParam(
                ctorParamName: "Age",
                m => m.MapFrom(src => src.DateOfBirth.CalculateAge())
            )
            .ReverseMap();
            //-- Neil-Version (he uses classes as Dtos)
            // .ForMember(
            //     dest => dest.PhotoUrl, 
            //     opt => opt.MapFrom(src => src.Photos.FirstOrDefault(ph => ph.IsMain).Url))
            // .ForMember(
            //     dest => dest.Age,
            //     opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge())
            // );
        CreateMap<Photo, PhotoDto>();
    }
}
