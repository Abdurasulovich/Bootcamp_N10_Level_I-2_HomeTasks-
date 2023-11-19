using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using N71.Blog.Application.Dtos;
using N71.Blog.Application.Foundations;
using N71.Blog.Application.Identity.Constants;
using N71.Blog.Application.ManagementServices.Interfaces;
using N71.Blog.Domain.Entity;

namespace N71.Blog.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly  IMapper _mapper;
    private readonly IBlogManagementService _blogManagementService;
    private readonly ICommentService _commentService;

    public CommentController(IMapper mapper,
        IBlogManagementService blogManagementService,
        ICommentService commentService)
    {
        _blogManagementService = blogManagementService;
        _mapper = mapper;
        _commentService = commentService;
    }

    [HttpGet("{blogId}")]
    public async ValueTask<IActionResult> GetCommentByBlogIdAsync([FromRoute] Guid blogId,
        CancellationToken cancellationToken)
    {

        var comments = await _blogManagementService.GetCommentsByBlogsIdAsync(blogId, cancellationToken);

        var result = comments.Select(_mapper.Map<CommentDto>);

        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateCommentAsync([FromBody] CommentDto commentDto,
        CancellationToken cancellationToken)
    {
        var comment = _mapper.Map<Comment>(commentDto);
        comment.UserId = Guid.Parse(User.Claims.First(claim => claim.Type == ClaimConstants.UserId).Value);

        return Ok(await _blogManagementService.CreateCommentAsync(comment, cancellationToken));
    }

    [HttpPut]
    public async ValueTask<IActionResult>
        UpdateCommentAsync([FromBody] CommentDto commentDto, CancellationToken cancellationToken)
    {
        var comment = _mapper.Map<Comment>(commentDto);
        comment.UserId = Guid.Parse(User.Claims.First(claim => claim.Type == ClaimConstants.UserId).Value);
        return Ok(await _commentService.UpdateAsync(comment, cancellationToken: cancellationToken));
    }

    [HttpDelete("{commentId}")]
    public async ValueTask<IActionResult> DeleteByIdAsync([FromRoute] Guid commentId,
        CancellationToken cancellationToken)
    {
        var authorId = Guid.Parse(User.Claims.First(claim => claim.Type == ClaimConstants.UserId).Value);

        return Ok(await _commentService.DeleteByIdAsync(commentId, authorId, cancellationToken: cancellationToken));
        
    }
}