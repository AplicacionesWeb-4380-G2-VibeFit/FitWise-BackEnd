using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

[ApiController]
[Route("api/v1/creator/{creatorId:int}/healthPlans")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available operations for managing health plans-creator in the FitWisePlatform.")]
public class CreatorHealthPlansController(IHealthPlanQueryService healthPlanQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all healthPlans by creator id",
        Description = "Get all healthPlans by creator id",
        OperationId = "GetHealthPlansByCreatorId")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns list of healthPlans", 
        typeof(IEnumerable<HealthPlanResource>))]
    public async Task<IActionResult> GetHealthPlansByCreatorId(int creatorId)
    {
        var getHealthPlansByCreatorIdQuery = new GetHealthPlansByCreatorIdQuery(creatorId);
        var healthPlans = await healthPlanQueryService.Handle(getHealthPlansByCreatorIdQuery);
        var healthPlanResources 
            = healthPlans.Select(HealthPlanResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(healthPlanResources);
    }
}