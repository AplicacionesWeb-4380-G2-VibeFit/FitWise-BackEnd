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
[SwaggerTag("Available operations for managing health plans in the FitWise Platform.")]
public class HealthPlanController
(
    IHealthPlanCommandService healthPlanCommandService,
    IHealthPlanQueryService healthPlanQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all health plans",
        Description = "Retrieves a list of all health plans available in the FitWise Platform.",
        OperationId = "GetAllHealthPlans")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns all available health plans", typeof(IEnumerable<HealthPlanResource>))]        
    public async Task<IActionResult> GetAllHealthPlans()
    {
        var healthPlans = await healthPlanQueryService.Handle(new GetAllHealthPlansQuery());
        var healthPlansResources = healthPlans.Select(HealthPlanResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(healthPlansResources);
    }
    
    [HttpGet("{healthPlanId:int}")]
    [SwaggerOperation(
        Summary = "Get health plan by Id",
        Description = "Retrieves a health plan available in the FitWise Platform.",
        OperationId = "GetHealthPlanById")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns a healthPlan", typeof(HealthPlanResource))]
    public async Task<IActionResult> GetHealthPlanById(int healthPlanId)
    {
        var getHealthPlanByIdQuery = new GetHealthPlanByIdQuery(healthPlanId);
        var healthPlan = await healthPlanQueryService.Handle(getHealthPlanByIdQuery);
        if (healthPlan is null)
        {
            return NotFound();
        }
        var healthPlanResource = HealthPlanResourceFromEntityAssembler.ToResourceFromEntity(healthPlan);
        return Ok(healthPlanResource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new health plan",
        Description = "Create a new health plan",
        OperationId = "CreateHealthPlan")
    ]
    [SwaggerResponse(StatusCodes.Status201Created, "The health plan was created", typeof(HealthPlanResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The health plan could not be created")]
    public async Task<IActionResult> CreateHealthPlan([FromBody] CreateHealthPlanResource resource)
    {
        var createdHealthPlanCommand = CreateHealthPlanCommandFromResourceAssembler.ToCommandFromResource(resource);
        var healthPlan = await healthPlanCommandService.Handle(createdHealthPlanCommand);
        if (healthPlan is null) return BadRequest();
        var healthPlanResource = HealthPlanResourceFromEntityAssembler.ToResourceFromEntity(healthPlan);
        return CreatedAtAction(nameof(GetHealthPlanById), new { healthPlanId = healthPlanResource.Id }, healthPlanResource);
    }
}