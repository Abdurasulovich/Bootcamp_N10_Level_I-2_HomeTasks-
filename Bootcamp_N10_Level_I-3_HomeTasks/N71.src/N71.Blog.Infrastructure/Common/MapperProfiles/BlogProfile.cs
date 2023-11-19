using AutoMapper;
using N71.Blog.Application.Dtos;
using N71.Blog.Domain.Entity;

namespace N71.Blog.Infrastructure.Common.MapperProfiles;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<BlogDto, Blogs>();
        CreateMap<Blogs, BlogDto>();
    }
}