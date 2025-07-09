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
public class ReviewsController(IReviewCommandService commandService, IReviewQueryService queryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await queryService.Handle(new GetAllReviewsQuery());
        return Ok(result.Select(ReviewResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await queryService.Handle(new GetReviewByIdQuery(id));
        return result == null ? NotFound() : Ok(ReviewResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet("by-health-plan/{healthPlanId:int}")]
    public async Task<IActionResult> GetByHealthPlanId(int healthPlanId)
    {
        var result = await queryService.Handle(new GetReviewsByHealthPlanIdQuery(healthPlanId));
        return Ok(result.Select(ReviewResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReviewResource resource)
    {
        var command = CreateReviewCommandFromResourceAssembler.ToCommandFromResource(resource);
        var created = await commandService.Handle(command);
        var reviewResource = ReviewResourceFromEntityAssembler.ToResourceFromEntity(created);
        return CreatedAtAction(nameof(GetById), new { id = reviewResource.Id }, reviewResource);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateReviewResource resource)
    {
        var command = new UpdateReviewCommand(id, resource.Score, resource.Description);
        var updated = await commandService.Handle(command);
        return updated == null ? NotFound() : Ok(ReviewResourceFromEntityAssembler.ToResourceFromEntity(updated));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await commandService.Handle(new DeleteReviewCommand(id));
        return success ? NoContent() : NotFound();
    }
}
