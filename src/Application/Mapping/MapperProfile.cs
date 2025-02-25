using Application.DTOs.Pet;
using Application.DTOs.PetAlert;
using Application.DTOs.Post;
using Application.DTOs.User;
using AutoMapper;
using Domain.PetAlerts;
using Domain.Pets;
using Domain.Posts;
using Domain.Users;

namespace Application.Mapping;
internal class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<ApplicationUser, UserDto>().ReverseMap();
        CreateMap<ApplicationUser, UserShortInfoDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName()))
            .ReverseMap();

        CreateMap<Pet, PetDto>().ReverseMap();
        CreateMap<PetWeight, PetWeightDto>().ReverseMap();
        CreateMap<PetVaccination, PetVaccinationDto>().ReverseMap();
        CreateMap<PetAlert, PetAlertDto>().ReverseMap();

        CreateMap<Post, PostDto>().ReverseMap();
    }
}
