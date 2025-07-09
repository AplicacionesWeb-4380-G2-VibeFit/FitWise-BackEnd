using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UPC.FitWisePlatform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Operations for assigning and managing exercises within a specific health plan.")]
public class HealthPlanExercisesController
(
    IHealthPlanExerciseCommandService healthPlanExerciseCommandService,
    IHealthPlanExerciseQueryService healthPlanExerciseQueryService) : ControllerBase
{
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get a specific health plan exercise assigment by its id.",
        Description = "Retrieves a single exercise assignment with its details",
        OperationId = "GetHealthPlanExerciseById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns the exercise assignment",
        typeof(HealthPlanExerciseResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound,
        "Exercise assignment not found, or does not belong to the specified health plan.")]
    public async Task<ActionResult> GetHealthPlanExerciseById(int id)
    {
        var getHealthPlanExerciseByIdQuery = new GetHealthPlanExerciseByIdQuery(id);
        var healthPlanExercise = await healthPlanExerciseQueryService.Handle(getHealthPlanExerciseByIdQuery);
        if (healthPlanExercise == null)
        {
            return NotFound();
        }

        var healthPlanExerciseResource =
            HealthPlanExerciseResourceFromEntityAssembler.ToResourceFromEntity(healthPlanExercise);
        return Ok(healthPlanExerciseResource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Assign an exercise to a health plan",
        Description = "Assigns an existing exercise to a specific health plan with details like day of week, sets, and reps.",
        OperationId = "AssignExerciseToHealthPlan"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "The exercise assignment was created",
        typeof(HealthPlanExerciseResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The exercise assignment could not be created due to invalid data.")]
    public async Task<ActionResult> AssignExerciseToHealthPlan([FromBody] AssignExerciseToHealthPlanResource resource)
    {
        var createdHealthPlanExerciseCommand = AssignExerciseToHealthPlanCommandFromResourceAssembler
            .ToCommandFromResource(resource);
        var healthPlanExercise = await healthPlanExerciseCommandService.Handle(createdHealthPlanExerciseCommand);
        if (healthPlanExercise is null) return BadRequest();
        var healthPlanExerciseResource = HealthPlanExerciseResourceFromEntityAssembler.ToResourceFromEntity(healthPlanExercise);
        return CreatedAtAction(
            nameof(GetHealthPlanExerciseById), new { id = healthPlanExerciseResource.Id },
            healthPlanExerciseResource
        );
    }
    
    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update health plan exercise by Id and UpdateResource",
        Description = "Retrieves the updated health plan exercise that is available in the FitWise Platform.",
        OperationId = "UpdateHealthPlanExercise")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns the updated health plan exercise",
        typeof(HealthPlanExerciseResource))]
    public async Task<IActionResult> UpdateHealthPlanExercise(
        int id, [FromBody] UpdateHealthPlanExerciseResource resource)
    {
        var updatedHealthPlanExerciseCommand = UpdateHealthPlanExerciseCommandFromResourceAssembler
            .ToCommandFromResource(id, resource);
        var healthPlanExercise = await healthPlanExerciseCommandService.Handle(updatedHealthPlanExerciseCommand);
        if (healthPlanExercise is null) return NotFound();
        var healthPlanExerciseResource = HealthPlanExerciseResourceFromEntityAssembler.ToResourceFromEntity(healthPlanExercise);
        return Ok(healthPlanExerciseResource);
    }
    
    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Delete health plan exercise by Id",
        Description = "Deletes a health plan exercise from the FitWise Platform.",
        OperationId = "DeleteHealthPlanExercise")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "The health plan exercise was successfully deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The health exercise exercise was not found")]
    public async Task<IActionResult> DeleteHealthPlanExercise(int id)
    {
        var deletedHealthPlanExerciseCommand = new DeleteHealthPlanExerciseCommand(id);
        var result = await healthPlanExerciseCommandService.Handle(deletedHealthPlanExerciseCommand);
        if (!result) return NotFound();
        return Ok($"Health plan exercise with ID {id} was deleted successfully.");
    }
}