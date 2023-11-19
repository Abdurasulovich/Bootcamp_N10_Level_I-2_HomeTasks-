using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using N71.Blog.Application.Dtos;
using N71.Blog.Application.Foundations;
using N71.Blog.Application.Identity.Constants;
using N71.Blog.Application.ManagementServices.Interfaces;
using N71.Blog.Domain.Entity;
using N71.Blog.Persistance.Repositories.Interfaces;

namespace N71.Blog.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;
    private readonly IMapper _mapper;
    private readonly IBlogManagementService _blogManagementService;

    public BlogController(
        IBlogService blogService,
        IMapper mapper,
        IBlogManagementService blogManagementService)
    {
        _blogManagementService = blogManagementService;
        _blogService = blogService;
        _mapper = mapper;
    }

    [HttpGet("bloggers/popular")]
    public async ValueTask<IActionResult> GetPopularBloggers(CancellationToken cancellationToken)
    {
        var bloggers = await _blogManagementService.GetPopularBloggersAsync(cancellationToken);
        var result = bloggers.Select(_mapper.Map<UserDto>);
        return result.Any() ? Ok(result) : NoContent();
    }

    [Authorize(Roles = "Admin,Author,Reader")]
    [HttpGet("{authorId}")]

    public async ValueTask<IActionResult> GetBlogsByAuthor([FromRoute] Guid authorId,
        CancellationToken cancellationToken)
    {
        var blogs = await _blogManagementService.GetBlogsByUserId(authorId, cancellationToken);
        var blogDtos = blogs.Select(_mapper.Map<BlogDto>);
        return blogDtos.Any() ? Ok(blogs) : NoContent();
    }

    [Authorize(Roles = "Reader")]
    [HttpPost]
    public async ValueTask<IActionResult> CreateBlogAsync([FromBody] BlogDto blogDto, CancellationToken cancellationToken)
    {
        var authorId = Guid.Parse(User.Claims.First(claim => claim.Type == ClaimConstants.UserId).Value);
        var blog = _mapper.Map<Blogs>(blogDto);
        blog.UserId = authorId;

        return Ok(await _blogService.CreateAsync(blog, cancellationToken : cancellationToken));
    }

}