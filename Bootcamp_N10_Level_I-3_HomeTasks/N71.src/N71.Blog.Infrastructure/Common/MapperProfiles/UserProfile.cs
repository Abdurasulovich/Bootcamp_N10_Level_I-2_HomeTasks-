using AutoMapper;
using N71.Blog.Application.Dtos;
using N71.Blog.Application.Identity.Models;
using N71.Blog.Domain.Entity;

namespace N71.Blog.Infrastructure.Common.MapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<SignUpDetails, User>();
        CreateMap<User, UserDto>().ReverseMap();
    }
}