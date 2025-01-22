using Application.DTOs.Pet;
using Application.DTOs.User;
using AutoMapper;
using Domain.Pets;
using Domain.Users;

namespace Application.Mapping;
internal class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<ApplicationUser, UserDto>().ReverseMap()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<Pet, PetDto>().ReverseMap();
        CreateMap<PetWeight, PetWeightDto>().ReverseMap();
        CreateMap<PetVaccination, PetVaccinationDto>().ReverseMap();
    }
}
