using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UPC.FitWisePlatform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using UPC.FitWisePlatform.API.Organizing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Organizing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Organizing.Domain.Services;
using UPC.FitWisePlatform.API.Organizing.Interfaces.REST.Resources;
using UPC.FitWisePlatform.API.Organizing.Interfaces.REST.Transform;

namespace UPC.FitWisePlatform.API.Organizing.Interfaces;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Operations for managing schedules in the FitWise Platform.")]
public class SchedulesController(IScheduleCommandService commandService, IScheduleQueryService queryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all schedules",
        Description = "Retrieves all schedules from the platform.",
        OperationId = "GetAllSchedules")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of schedules", typeof(IEnumerable<ScheduleResource>))]
    public async Task<IActionResult> GetAll()
    {
        var result = await queryService.Handle(new GetAllSchedulesQuery());
        return Ok(result.Select(ScheduleResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get schedule by ID",
        Description = "Retrieves a schedule by its unique identifier.",
        OperationId = "GetScheduleById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns the schedule", typeof(ScheduleResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Schedule not found")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await queryService.Handle(new GetScheduleByIdQuery(id));
        return result == null ? NotFound() : Ok(ScheduleResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new schedule",
        Description = "Creates a new schedule in the system.",
        OperationId = "CreateSchedule")]
    [SwaggerResponse(StatusCodes.Status201Created, "The schedule was created successfully", typeof(ScheduleResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid schedule data")]
    public async Task<IActionResult> Create([FromBody] CreateScheduleResource resource)
    {
        var command = CreateScheduleCommandFromResourceAssembler.ToCommandFromResource(resource);
        var created = await commandService.Handle(command);
        var scheduleResource = ScheduleResourceFromEntityAssembler.ToResourceFromEntity(created);
        return CreatedAtAction(nameof(GetById), new { id = scheduleResource.Id }, scheduleResource);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update a schedule",
        Description = "Updates the date of a specific schedule.",
        OperationId = "UpdateSchedule")]
    [SwaggerResponse(StatusCodes.Status200OK, "The schedule was updated successfully", typeof(ScheduleResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Schedule not found")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateScheduleResource resource)
    {
        var command = new UpdateScheduleCommand(id, resource.Date);
        var updated = await commandService.Handle(command);
        return updated == null ? NotFound() : Ok(ScheduleResourceFromEntityAssembler.ToResourceFromEntity(updated));
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Delete a schedule",
        Description = "Deletes a schedule from the system.",
        OperationId = "DeleteSchedule")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The schedule was deleted successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Schedule not found")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await commandService.Handle(new DeleteScheduleCommand(id));
        return success ? NoContent() : NotFound();
    }
}
