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
[SwaggerTag("Available operations for managing reviews in the FitWise Platform.")]
public class ReviewsController(IReviewCommandService commandService, IReviewQueryService queryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all reviews",
        Description = "Retrieves all reviews registered in the FitWise Platform.",
        OperationId = "GetAllReviews")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of reviews", typeof(IEnumerable<ReviewResource>))]
    public async Task<IActionResult> GetAll()
    {
        var result = await queryService.Handle(new GetAllReviewsQuery());
        return Ok(result.Select(ReviewResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get review by Id",
        Description = "Retrieves a review by its unique identifier.",
        OperationId = "GetReviewById")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns the review", typeof(ReviewResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Review not found")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await queryService.Handle(new GetReviewByIdQuery(id));
        return result == null ? NotFound() : Ok(ReviewResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet("by-health-plan/{healthPlanId:int}")]
    [SwaggerOperation(
        Summary = "Get reviews by Health Plan Id",
        Description = "Retrieves all reviews associated with a specific health plan.",
        OperationId = "GetReviewsByHealthPlanId")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of reviews", typeof(IEnumerable<ReviewResource>))]
    public async Task<IActionResult> GetByHealthPlanId(int healthPlanId)
    {
        var result = await queryService.Handle(new GetReviewsByHealthPlanIdQuery(healthPlanId));
        return Ok(result.Select(ReviewResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new review",
        Description = "Creates a new review for a health plan.",
        OperationId = "CreateReview")
    ]
    [SwaggerResponse(StatusCodes.Status201Created, "The review was created successfully", typeof(ReviewResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid review data")]
    public async Task<IActionResult> Create([FromBody] CreateReviewResource resource)
    {
        var command = CreateReviewCommandFromResourceAssembler.ToCommandFromResource(resource);
        var created = await commandService.Handle(command);
        var reviewResource = ReviewResourceFromEntityAssembler.ToResourceFromEntity(created);
        return CreatedAtAction(nameof(GetById), new { id = reviewResource.Id }, reviewResource);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update review by Id",
        Description = "Updates an existing review.",
        OperationId = "UpdateReview")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "The review was updated", typeof(ReviewResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Review not found")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateReviewResource resource)
    {
        var command = new UpdateReviewCommand(id, resource.Score, resource.Description);
        var updated = await commandService.Handle(command);
        return updated == null ? NotFound() : Ok(ReviewResourceFromEntityAssembler.ToResourceFromEntity(updated));
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Delete review by Id",
        Description = "Deletes a review by its unique identifier.",
        OperationId = "DeleteReview")
    ]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The review was deleted successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Review not found")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await commandService.Handle(new DeleteReviewCommand(id));
        return success ? NoContent() : NotFound();
    }
}
