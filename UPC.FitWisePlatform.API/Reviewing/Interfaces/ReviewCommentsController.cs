using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using UPC.FitWisePlatform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Reviewing.Domain.Services;
using UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Resources;
using UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Transform;

namespace UPC.FitWisePlatform.API.Reviewing.Interfaces;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available operations for managing review comments in the FitWise Platform.")]
public class ReviewCommentsController(IReviewCommentCommandService commandService, IReviewCommentQueryService queryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all review comments",
        Description = "Retrieves all review comments available in the FitWise Platform.",
        OperationId = "GetAllReviewComments"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of review comments", typeof(IEnumerable<ReviewCommentResource>))]
    public async Task<IActionResult> GetAll()
    {
        var result = await queryService.Handle(new GetAllReviewCommentsQuery());
        return Ok(result.Select(ReviewCommentResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("by-review/{reviewId:int}")]
    [SwaggerOperation(
        Summary = "Get review comments by review Id",
        Description = "Retrieves all review comments associated with a specific review.",
        OperationId = "GetReviewCommentsByReviewId"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of review comments for the given review", typeof(IEnumerable<ReviewCommentResource>))]
    public async Task<IActionResult> GetByReviewId(int reviewId)
    {
        var result = await queryService.Handle(new GetReviewCommentsByReviewIdQuery(reviewId));
        return Ok(result.Select(ReviewCommentResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new review comment",
        Description = "Creates a new review comment in the FitWise Platform.",
        OperationId = "CreateReviewComment"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "The review comment was created successfully", typeof(ReviewCommentResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid review comment data")]
    public async Task<IActionResult> Create([FromBody] CreateReviewCommentResource resource)
    {
        var command = CreateReviewCommentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var created = await commandService.Handle(command);
        var commentResource = ReviewCommentResourceFromEntityAssembler.ToResourceFromEntity(created);
        return CreatedAtAction(nameof(GetByReviewId), new { reviewId = commentResource.ReviewId }, commentResource);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update a review comment by Id",
        Description = "Updates the content of an existing review comment.",
        OperationId = "UpdateReviewComment"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The review comment was updated successfully", typeof(ReviewCommentResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Review comment not found")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateReviewCommentResource resource)
    {
        var command = new UpdateReviewCommentCommand(id, resource.Content);
        var updated = await commandService.Handle(command);
        return updated == null ? NotFound() : Ok(ReviewCommentResourceFromEntityAssembler.ToResourceFromEntity(updated));
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Delete a review comment by Id",
        Description = "Deletes a review comment from the FitWise Platform.",
        OperationId = "DeleteReviewComment"
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The review comment was successfully deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Review comment not found")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await commandService.Handle(new DeleteReviewCommentCommand(id));
        return success ? NoContent() : NotFound();
    }
}
