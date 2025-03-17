using Application.DTOs.Advertisement;
using Application.DTOs.Comment;
using Application.DTOs.Notification;
using Application.DTOs.Pet;
using Application.DTOs.PetAlert;
using Application.DTOs.Post;
using Application.DTOs.Reaction;
using Application.DTOs.User;
using AutoMapper;
using Domain.Advertisement;
using Domain.Comments;
using Domain.Notifications;
using Domain.PetAlerts;
using Domain.Pets;
using Domain.Posts;
using Domain.Reactions;
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
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<Reaction, ReactionDto>().ReverseMap();

        CreateMap<Notification, NotificationDto>().ReverseMap();

        CreateMap<Ad, AdDto>().ReverseMap();
        CreateMap<Ad, AdStatisticsDto>().ReverseMap();
    }
}
