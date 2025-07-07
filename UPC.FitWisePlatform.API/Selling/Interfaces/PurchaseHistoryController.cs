using Microsoft.AspNetCore.Mvc;
using UPC.FitWisePlatform.API.Selling.Application.Internal.CommandServices;
using UPC.FitWisePlatform.API.Selling.Application.Internal.QueryServices;
using UPC.FitWisePlatform.API.Selling.Domain.Model.Commands;

namespace UPC.FitWisePlatform.API.Selling.Interfaces;

[ApiController]
[Route("api/v1/purchase-history")]
public class PurchaseHistoryController(
    PurchaseHistoryCommandService commandService,
    PurchaseHistoryQueryService queryService)
    : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var result = await queryService.GetByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePurchaseHistoryCommand command)
    {
        var result = await commandService.CreateAsync(command);
        return CreatedAtAction(nameof(GetByUserId), new { userId = result.Id }, result);
    }

    [HttpPatch("{userId}")]
    public async Task<IActionResult> AddPayment(string userId, [FromBody] AddPaymentToHistoryCommand command)
    {
        var result = await commandService.AddPaymentToHistoryAsync(userId, command);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var histories = await queryService.GetAllAsync();
        return Ok(histories);
    }

}