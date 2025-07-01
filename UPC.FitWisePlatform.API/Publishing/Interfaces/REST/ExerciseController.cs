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
[SwaggerTag("Available operations for managing exercises in the FitWise Platform.")]
public class ExerciseController
(
    IExerciseCommandService  exerciseCommandService,
    IExerciseQueryService  exerciseQueryService) : ControllerBase
{
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get exercise by Id",
        Description = "Retrieves a exercise available in the FitWise Platform.",
        OperationId = "GetExerciseById")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns a exercise", typeof(ExerciseResource))]
    public async Task<IActionResult> GetExerciseById(int id)
    {
        var getExerciseByIdQuery = new GetExerciseByIdQuery(id);
        var exercise = await exerciseQueryService.Handle(getExerciseByIdQuery);
        if (exercise is null)
        {
            return NotFound();
        }
        var exerciseResource = ExerciseResourceFromEntityAssembler.ToResourceFromEntity(exercise);
        return Ok(exerciseResource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new exercise",
        Description = "Create a new exercise",
        OperationId = "CreateExercise")
    ]
    [SwaggerResponse(StatusCodes.Status201Created, "The exercise was created", 
        typeof(ExerciseResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The exercise could not be created")]
    public async Task<IActionResult> CreateExercise([FromBody] CreateExerciseResource resource)
    {
        var createdExerciseCommand = CreateExerciseCommandFromResourceAssembler.ToCommandFromResource(resource);
        var exercise = await exerciseCommandService.Handle(createdExerciseCommand);
        if (exercise is null) return BadRequest();
        var exerciseResource = ExerciseResourceFromEntityAssembler.ToResourceFromEntity(exercise);
        return CreatedAtAction(nameof(GetExerciseById), 
            new { id = exerciseResource.Id }, exerciseResource);
    }
    
    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update exercise by Id and UpdateResource",
        Description = "Retrieves the updated exercise that is available in the FitWise Platform.",
        OperationId = "UpdateExercise")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns the updated exercise", typeof(ExerciseResource))]
    public async Task<IActionResult> UpdateExercise(int id, [FromBody] UpdateExerciseResource resource)
    {
        var updatedExerciseCommand = UpdateExerciseCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var exercise = await exerciseCommandService.Handle(updatedExerciseCommand);
        if (exercise is null) return NotFound();
        var exerciseResource = ExerciseResourceFromEntityAssembler.ToResourceFromEntity(exercise);
        return Ok(exerciseResource);
    }

}