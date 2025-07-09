using Microsoft.AspNetCore.Mvc;
using UPC.FitWisePlatform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using UPC.FitWisePlatform.API.Selling.Application.Internal.QueryServices;
using UPC.FitWisePlatform.API.Selling.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Selling.Application.Internal.CommandServices;

namespace UPC.FitWisePlatform.API.Selling.Interfaces;

[Authorize]
[ApiController]
[Route("api/v1/purchased-plans")]
public class PurchasedPlansController(
    PurchasedPlanCommandService commandService,
    PurchasedPlanQueryService queryService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await queryService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await queryService.GetByIdAsync(id);
        return result != null
            ? Ok(result)
            : NotFound(new { message = $"Purchased plan with id {id} not found." });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePurchasedPlanCommand command)
    {
        var newId = await commandService.CreateAsync(command);
        return Ok(new { message = "Purchased plan created successfully.", id = newId });
    }
    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePurchasedPlanCommand command)
    {
        var updated = await commandService.UpdateAsync(id, command);
        return updated
            ? Ok(new { message = $"Purchased plan {id} updated successfully." })
            : NotFound(new { message = $"Purchased plan with id {id} not found." });
    }




    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await commandService.DeleteAsync(id);
        return success
            ? Ok(new { message = $"Purchased plan {id} deleted successfully." })
            : NotFound(new { message = $"Purchased plan with id {id} not found." });
    }


}
