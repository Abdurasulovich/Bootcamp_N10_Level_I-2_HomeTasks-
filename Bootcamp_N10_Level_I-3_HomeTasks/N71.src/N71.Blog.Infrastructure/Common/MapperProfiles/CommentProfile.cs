using AutoMapper;
using N71.Blog.Application.Dtos;
using N71.Blog.Domain.Entity;

namespace N71.Blog.Infrastructure.Common.MapperProfiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.Date, options =>
                options.MapFrom(src => src.ModifiedTime != null ? src.ModifiedTime : src.CreatedTime))
            .ReverseMap();
    }
}