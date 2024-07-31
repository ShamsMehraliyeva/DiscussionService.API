using Application.Utilities.JWT;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles;

public class AuthMappingProfile:Profile
{
    public AuthMappingProfile()
    {
        CreateMap<RefreshToken, RefreshTokenModel>().ReverseMap();
    }
}