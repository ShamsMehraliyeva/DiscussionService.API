using Application.Features.Topics.Commands.AddComment;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles;

public class CommentMappingProfile: Profile
{
    public CommentMappingProfile()
    {
       // CreateMap<AddCommentCommand, Comment>().ReverseMap();
    }
}