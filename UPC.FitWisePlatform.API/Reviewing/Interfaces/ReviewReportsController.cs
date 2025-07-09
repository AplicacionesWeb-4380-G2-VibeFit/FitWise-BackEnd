using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
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
public class ReviewReportsController(IReviewReportCommandService commandService, IReviewReportQueryService queryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await queryService.Handle(new GetAllReviewReportsQuery());
        return Ok(result.Select(ReviewReportResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("by-review/{reviewId:int}")]
    public async Task<IActionResult> GetByReviewId(int reviewId)
    {
        var result = await queryService.Handle(new GetReviewReportsByReviewIdQuery(reviewId));
        return Ok(result.Select(ReviewReportResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReviewReportResource resource)
    {
        var command = CreateReviewReportCommandFromResourceAssembler.ToCommandFromResource(resource);
        var created = await commandService.Handle(command);
        var reportResource = ReviewReportResourceFromEntityAssembler.ToResourceFromEntity(created);
        return CreatedAtAction(nameof(GetByReviewId), new { reviewId = reportResource.ReviewId }, reportResource);
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
    {
        var command = new UpdateReviewReportStatusCommand(id, status);
        var updated = await commandService.Handle(command);
        return updated == null ? NotFound() : Ok(ReviewReportResourceFromEntityAssembler.ToResourceFromEntity(updated));
    }
}
