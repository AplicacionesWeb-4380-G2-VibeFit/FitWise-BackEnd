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
[SwaggerTag("Available operations for managing review reports in the FitWise Platform.")]
public class ReviewReportsController(IReviewReportCommandService commandService, IReviewReportQueryService queryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all review reports",
        Description = "Retrieves all review reports available in the FitWise Platform.",
        OperationId = "GetAllReviewReports"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of review reports", typeof(IEnumerable<ReviewReportResource>))]
    public async Task<IActionResult> GetAll()
    {
        var result = await queryService.Handle(new GetAllReviewReportsQuery());
        return Ok(result.Select(ReviewReportResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("by-review/{reviewId:int}")]
    [SwaggerOperation(
        Summary = "Get review reports by review Id",
        Description = "Retrieves all review reports associated with a specific review.",
        OperationId = "GetReviewReportsByReviewId"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of review reports for the given review", typeof(IEnumerable<ReviewReportResource>))]
    public async Task<IActionResult> GetByReviewId(int reviewId)
    {
        var result = await queryService.Handle(new GetReviewReportsByReviewIdQuery(reviewId));
        return Ok(result.Select(ReviewReportResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new review report",
        Description = "Creates a new review report in the FitWise Platform.",
        OperationId = "CreateReviewReport"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "The review report was created successfully", typeof(ReviewReportResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid review report data")]
    public async Task<IActionResult> Create([FromBody] CreateReviewReportResource resource)
    {
        var command = CreateReviewReportCommandFromResourceAssembler.ToCommandFromResource(resource);
        var created = await commandService.Handle(command);
        var reportResource = ReviewReportResourceFromEntityAssembler.ToResourceFromEntity(created);
        return CreatedAtAction(nameof(GetByReviewId), new { reviewId = reportResource.ReviewId }, reportResource);
    }

    [HttpPatch("{id:int}/status")]
    [SwaggerOperation(
        Summary = "Update the status of a review report",
        Description = "Updates the status field of an existing review report.",
        OperationId = "UpdateReviewReportStatus"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The review report status was updated successfully", typeof(ReviewReportResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Review report not found")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
    {
        var command = new UpdateReviewReportStatusCommand(id, status);
        var updated = await commandService.Handle(command);
        return updated == null ? NotFound() : Ok(ReviewReportResourceFromEntityAssembler.ToResourceFromEntity(updated));
    }
}
