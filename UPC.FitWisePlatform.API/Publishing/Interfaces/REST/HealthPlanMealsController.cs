using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Operations for assigning and managing meals within a specific health plan.")]
public class HealthPlanMealsController
(
    IHealthPlanMealCommandService healthPlanMealCommandService,
    IHealthPlanMealQueryService healthPlanMealQueryService) : ControllerBase
{
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get a specific health plan meal assignment by ID",
        Description = "Retrieves details of a single meal assignment within a health plan using its unique assignment ID.",
        OperationId = "GetHealthPlanMealById"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns the meal assignment", 
        typeof(HealthPlanMealResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, 
        "Meal assignment not found, or does not belong to the specified health plan.")]
    public async Task<IActionResult> GetHealthPlanMealById(int id)
    {
        var getHealthPlanMealByIdQuery = new GetHealthPlanMealByIdQuery(id);
        var healthPlanMeal = await healthPlanMealQueryService.Handle(getHealthPlanMealByIdQuery);
        if (healthPlanMeal == null)
        {
            return NotFound();
        }
        var healthPlanMealResource = HealthPlanMealResourceFromEntityAssembler
            .ToResourceFromEntity(healthPlanMeal);
        return Ok(healthPlanMealResource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Assign an meal to a health plan",
        Description = "Assigns an existing meal to a specific health plan with details.",
        OperationId = "AssignMealToHealthPlan"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "The meal assignment was created",
        typeof(HealthPlanExerciseResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, 
        "The meal assignment could not be created due to invalid data.")]
    public async Task<ActionResult> AssignMealToHealthPlan([FromBody] AssignMealToHealthPlanResource resource)
    {
        var createdHealthPlanMealCommand = AssignMealToHealthPlanCommandFromResourceAssembler
            .ToCommandFromResource(resource);
        var healthPlanMeal = await healthPlanMealCommandService.Handle(createdHealthPlanMealCommand);
        if (healthPlanMeal is null) return BadRequest();
        var healthPlanMealResource = HealthPlanMealResourceFromEntityAssembler.ToResourceFromEntity(healthPlanMeal);
        return CreatedAtAction(
            nameof(GetHealthPlanMealById), new { id = healthPlanMealResource.Id }, healthPlanMealResource);
    }
    
    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update health plan meal by Id and UpdateResource",
        Description = "Retrieves the updated health plan meal that is available in the FitWise Platform.",
        OperationId = "UpdateHealthPlanMeal")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns the updated health plan meal",
        typeof(HealthPlanMealResource))]
    public async Task<IActionResult> UpdateHealthPlanMeal(
        int id, [FromBody] UpdateHealthPlanMealResource resource)
    {
        var updatedHealthPlanMealCommand = UpdateHealthPlanMealCommandFromResourceAssembler
            .ToCommandFromResource(id, resource);
        var healthPlanMeal = await healthPlanMealCommandService.Handle(updatedHealthPlanMealCommand);
        if (healthPlanMeal is null) return NotFound();
        var healthPlanMealResource = HealthPlanMealResourceFromEntityAssembler.ToResourceFromEntity(healthPlanMeal);
        return Ok(healthPlanMealResource);
    }
    
    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Delete health plan meal by Id",
        Description = "Deletes a health plan meal from the FitWise Platform.",
        OperationId = "DeleteHealthPlanMeal")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "The health plan meal was successfully deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The health plan meal was not found")]
    public async Task<IActionResult> DeleteHealthPlanMeal(int id)
    {
        var deletedHealthPlanMealCommand = new DeleteHealthPlanMealCommand(id);
        var result = await healthPlanMealCommandService.Handle(deletedHealthPlanMealCommand);
        if (!result) return NotFound();
        return Ok($"Health plan meal with ID {id} was deleted successfully.");
    }
}