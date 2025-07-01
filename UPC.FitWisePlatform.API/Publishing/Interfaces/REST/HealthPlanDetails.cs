using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST;

[ApiController]
[Route("api/v1/health-plans/{healthPlanId:int}")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Operations for managing details within a specific health plan.")]
public class HealthPlanDetails(IHealthPlanExerciseQueryService healthPlanExerciseQueryService,
    IHealthPlanMealQueryService healthPlanMealQueryService) : ControllerBase 
{
    [HttpGet("exercises")]
    [SwaggerOperation(
        Summary = "Get all health plan exercises by health plan id",
        Description = "Get all health plan exercises by health plan id",
        OperationId = "GetHealthPlanExercisesByHealthPlanId")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns list of health plan exercises",
        typeof(IEnumerable<HealthPlanExerciseResource>))]
    public async Task<IActionResult> GetHealthPlanExercisesByHealthPlanId(int healthPlanId)
    {
        var getHealthPlanExercisesByHealthPlanIdQuery = new GetHealthPlanExercisesByHealthPlanIdQuery(healthPlanId);
        var healthPlanExercises =
            await healthPlanExerciseQueryService.Handle(getHealthPlanExercisesByHealthPlanIdQuery);
        var healthPlanExerciseResources =
            healthPlanExercises.Select(HealthPlanExerciseResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(healthPlanExerciseResources);
    }
    
    [HttpGet("meals")]
    [SwaggerOperation(
        Summary = "Get all health plan meals by health plan id",
        Description = "Get all health plan meals by health plan id",
        OperationId = "GetHealthPlanMealsByHealthPlanId")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns list of health plan meals",
        typeof(IEnumerable<HealthPlanMealResource>))]
    public async Task<IActionResult> GetHealthPlanMealsByHealthPlanId(int healthPlanId)
    {
        var getHealthPlanMealsByHealthPlanIdQuery = new GetHealthPlanMealsByHealthPlanIdQuery(healthPlanId);
        var healthPlanMeals =
            await healthPlanMealQueryService.Handle(getHealthPlanMealsByHealthPlanIdQuery);
        var healthPlanMealResources =
            healthPlanMeals.Select(HealthPlanMealResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(healthPlanMealResources);
    }
}