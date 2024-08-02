using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Application.MappingProfiles;

public class CommentMappingProfile : Profile
{
    public CommentMappingProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<Topic, TopicDto>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));
        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
    }
}