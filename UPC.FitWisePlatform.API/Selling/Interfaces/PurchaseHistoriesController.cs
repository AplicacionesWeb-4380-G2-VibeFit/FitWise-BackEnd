using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UPC.FitWisePlatform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using UPC.FitWisePlatform.API.Selling.Application.Internal.CommandServices;
using UPC.FitWisePlatform.API.Selling.Application.Internal.QueryServices;
using UPC.FitWisePlatform.API.Selling.Domain.Model.Commands;

namespace UPC.FitWisePlatform.API.Selling.Interfaces;

[Authorize]
[ApiController]
[Route("api/v1/purchase-histories")]
[SwaggerTag("Operations for managing purchase histories in the FitWise Platform.")]
public class PurchaseHistoriesController(
    PurchaseHistoryCommandService commandService,
    PurchaseHistoryQueryService queryService)
    : ControllerBase
{
    [HttpGet("{userId}")]
    [SwaggerOperation(
        Summary = "Get purchase history by user ID",
        Description = "Retrieves the purchase history for a specific user.",
        OperationId = "GetPurchaseHistoryByUserId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns the purchase history for the user")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var result = await queryService.GetByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a purchase history",
        Description = "Creates a new purchase history record.",
        OperationId = "CreatePurchaseHistory")]
    [SwaggerResponse(StatusCodes.Status201Created, "The purchase history was created successfully")]
    public async Task<IActionResult> Create([FromBody] CreatePurchaseHistoryCommand command)
    {
        var result = await commandService.CreateAsync(command);
        return CreatedAtAction(nameof(GetByUserId), new { userId = result.Id }, result);
    }

    [HttpPatch("{userId}")]
    [SwaggerOperation(
        Summary = "Add a payment to purchase history",
        Description = "Adds a payment record to the user's purchase history.",
        OperationId = "AddPaymentToPurchaseHistory")]
    [SwaggerResponse(StatusCodes.Status200OK, "Payment added to purchase history successfully")]
    public async Task<IActionResult> AddPayment(string userId, [FromBody] AddPaymentToHistoryCommand command)
    {
        var result = await commandService.AddPaymentToHistoryAsync(userId, command);
        return Ok(result);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all purchase histories",
        Description = "Retrieves all purchase history records.",
        OperationId = "GetAllPurchaseHistories")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of purchase histories")]
    public async Task<IActionResult> GetAll()
    {
        var histories = await queryService.GetAllAsync();
        return Ok(histories);
    }
}
