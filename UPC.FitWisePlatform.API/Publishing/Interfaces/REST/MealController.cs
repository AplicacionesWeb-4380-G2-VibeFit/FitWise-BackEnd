using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available operations for managing meals in the FitWise Platform.")]
public class MealController
(
    IMealCommandService  mealCommandService,
    IMealQueryService  mealQueryService) : ControllerBase
{
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get meal by Id",
        Description = "Retrieves a meal available in the FitWise Platform.",
        OperationId = "GetMealById")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns a meal", typeof(MealResource))]
    public async Task<IActionResult> GetMealById(int id)
    {
        var getMealByIdQuery = new GetMealByIdQuery(id);
        var meal = await mealQueryService.Handle(getMealByIdQuery);
        if (meal is null)
        {
            return NotFound();
        }
        var mealResource = MealResourceFromEntityAssembler.ToResourceFromEntity(meal);
        return Ok(mealResource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new meal",
        Description = "Create a new meal",
        OperationId = "CreateMeal")
    ]
    [SwaggerResponse(StatusCodes.Status201Created, "The meal was created", typeof(MealResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The meal could not be created")]
    public async Task<IActionResult> CreateMeal([FromBody] CreateMealResource resource)
    {
        var createdMealCommand = CreateMealCommandFromResourceAssembler.ToCommandFromResource(resource);
        var meal = await mealCommandService.Handle(createdMealCommand);
        if (meal is null) return BadRequest();
        var mealResource = MealResourceFromEntityAssembler.ToResourceFromEntity(meal);
        return CreatedAtAction(nameof(GetMealById), new { id = mealResource.Id }, mealResource);
    }
    
    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update meal by Id and UpdateResource",
        Description = "Retrieves the updated meal that is available in the FitWise Platform.",
        OperationId = "UpdateMeal")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns the updated meal", typeof(MealResource))]
    public async Task<IActionResult> UpdateMeal(int id, [FromBody] UpdateMealResource resource)
    {
        var updatedMealCommand = UpdateMealCommandResourceFromEntityAssembler.ToCommandFromResource(id, resource);
        var meal = await mealCommandService.Handle(updatedMealCommand);
        if (meal is null) return NotFound();
        var mealResource = MealResourceFromEntityAssembler.ToResourceFromEntity(meal);
        return Ok(mealResource);
    }
}