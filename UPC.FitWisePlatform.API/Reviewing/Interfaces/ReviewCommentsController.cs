using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Reviewing.Domain.Services;
using UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Resources;
using UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Transform;

namespace UPC.FitWisePlatform.API.Reviewing.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ReviewCommentsController(IReviewCommentCommandService commandService, IReviewCommentQueryService queryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await queryService.Handle(new GetAllReviewCommentsQuery());
        return Ok(result.Select(ReviewCommentResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("by-review/{reviewId:int}")]
    public async Task<IActionResult> GetByReviewId(int reviewId)
    {
        var result = await queryService.Handle(new GetReviewCommentsByReviewIdQuery(reviewId));
        return Ok(result.Select(ReviewCommentResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReviewCommentResource resource)
    {
        var command = CreateReviewCommentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var created = await commandService.Handle(command);
        var commentResource = ReviewCommentResourceFromEntityAssembler.ToResourceFromEntity(created);
        return CreatedAtAction(nameof(GetByReviewId), new { reviewId = commentResource.ReviewId }, commentResource);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateReviewCommentResource resource)
    {
        var command = new UpdateReviewCommentCommand(id, resource.Content);
        var updated = await commandService.Handle(command);
        return updated == null ? NotFound() : Ok(ReviewCommentResourceFromEntityAssembler.ToResourceFromEntity(updated));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await commandService.Handle(new DeleteReviewCommentCommand(id));
        return success ? NoContent() : NotFound();
    }
}
