using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using UPC.FitWisePlatform.API.Organizing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Organizing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Organizing.Domain.Services;
using UPC.FitWisePlatform.API.Organizing.Interfaces.REST.Resources;
using UPC.FitWisePlatform.API.Organizing.Interfaces.REST.Transform;

namespace UPC.FitWisePlatform.API.Organizing.Interfaces;


[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class SchedulesController(IScheduleCommandService commandService, IScheduleQueryService queryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await queryService.Handle(new GetAllSchedulesQuery());
        return Ok(result.Select(ScheduleResourceFromEntityAssembler.ToResourceFromEntity));
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await queryService.Handle(new GetScheduleByIdQuery(id));
        return result == null ? NotFound() : Ok(ScheduleResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateScheduleResource resource)
    {
        var command = CreateScheduleCommandFromResourceAssembler.ToCommandFromResource(resource);
        var created = await commandService.Handle(command);
        var scheduleResource = ScheduleResourceFromEntityAssembler.ToResourceFromEntity(created);
        return CreatedAtAction(nameof(GetById), new { id = scheduleResource.Id }, scheduleResource);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateScheduleResource resource)
    {
        var command = new UpdateScheduleCommand(id, resource.Date);
        var updated = await commandService.Handle(command);
        return updated == null ? NotFound() : Ok(ScheduleResourceFromEntityAssembler.ToResourceFromEntity(updated));
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await commandService.Handle(new DeleteScheduleCommand(id));
        return success ? NoContent() : NotFound();
    }
}