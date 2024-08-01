using Application.Features.Topics.Commands.CreateTopic;
using Application.Features.Topics.Queries.GetTopicById;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Application.MappingProfiles;

public class TopicMappingProfile:Profile
{
    public TopicMappingProfile()
    {
        CreateMap<CreateTopicCommand, Topic>().ReverseMap();
        CreateMap<TopicDto, Topic>().ReverseMap();
        CreateMap<GetTopicByIdQueryResponse, Topic>().ReverseMap();
    }
}