using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UPC.FitWisePlatform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using UPC.FitWisePlatform.API.Selling.Application.Internal.QueryServices;
using UPC.FitWisePlatform.API.Selling.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Selling.Application.Internal.CommandServices;

namespace UPC.FitWisePlatform.API.Selling.Interfaces;

[Authorize]
[ApiController]
[Route("api/v1/purchased-plans")]
[SwaggerTag("Operations for managing purchased plans in the FitWise Platform.")]
public class PurchasedPlansController(
    PurchasedPlanCommandService commandService,
    PurchasedPlanQueryService queryService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all purchased plans",
        Description = "Retrieves all purchased plans in the system.",
        OperationId = "GetAllPurchasedPlans")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of purchased plans")]
    public async Task<IActionResult> GetAll()
    {
        var result = await queryService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get purchased plan by ID",
        Description = "Retrieves a purchased plan by its ID.",
        OperationId = "GetPurchasedPlanById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns the purchased plan")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Purchased plan not found")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await queryService.GetByIdAsync(id);
        return result != null
            ? Ok(result)
            : NotFound(new { message = $"Purchased plan with id {id} not found." });
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a purchased plan",
        Description = "Registers a new purchased plan.",
        OperationId = "CreatePurchasedPlan")]
    [SwaggerResponse(StatusCodes.Status200OK, "Purchased plan created successfully")]
    public async Task<IActionResult> Create([FromBody] CreatePurchasedPlanCommand command)
    {
        var newId = await commandService.CreateAsync(command);
        return Ok(new { message = "Purchased plan created successfully.", id = newId });
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a purchased plan",
        Description = "Updates an existing purchased plan by its ID.",
        OperationId = "UpdatePurchasedPlan")]
    [SwaggerResponse(StatusCodes.Status200OK, "Purchased plan updated successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Purchased plan not found")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePurchasedPlanCommand command)
    {
        var updated = await commandService.UpdateAsync(id, command);
        return updated
            ? Ok(new { message = $"Purchased plan {id} updated successfully." })
            : NotFound(new { message = $"Purchased plan with id {id} not found." });
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a purchased plan",
        Description = "Deletes a purchased plan by its ID.",
        OperationId = "DeletePurchasedPlan")]
    [SwaggerResponse(StatusCodes.Status200OK, "Purchased plan deleted successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Purchased plan not found")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await commandService.DeleteAsync(id);
        return success
            ? Ok(new { message = $"Purchased plan {id} deleted successfully." })
            : NotFound(new { message = $"Purchased plan with id {id} not found." });
    }
}
