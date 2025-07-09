using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations; // ✅ Necesario para Swagger
using UPC.FitWisePlatform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using UPC.FitWisePlatform.API.Selling.Application.Internal.CommandServices;
using UPC.FitWisePlatform.API.Selling.Application.Internal.QueryServices;
using UPC.FitWisePlatform.API.Selling.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Selling.Interfaces;

[Authorize]
[ApiController]
[Route("api/v1/payments")]
[SwaggerTag("Operations for managing payments in the FitWise Platform.")]
public class PaymentsController(
    PaymentCommandService paymentCommandService,
    PaymentQueryService paymentQueryService)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new payment",
        Description = "Creates a new payment record.",
        OperationId = "CreatePayment")]
    [SwaggerResponse(StatusCodes.Status200OK, "Payment created successfully")]
    public async Task<IActionResult> Create([FromBody] CreatePaymentCommand command)
    {
        var newId = await paymentCommandService.CreateAsync(command);
        return Ok(new
        {
            message = "Payment created successfully.",
            id = newId
        });
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all payments",
        Description = "Retrieves all payment records.",
        OperationId = "GetAllPayments")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of all payments")]
    public async Task<IActionResult> GetAll()
    {
        var payments = await paymentQueryService.GetAllAsync();
        return Ok(payments);
    }

    [HttpPatch("{id}")]
    [SwaggerOperation(
        Summary = "Update payment status",
        Description = "Updates the status of a specific payment.",
        OperationId = "PatchPaymentStatus")]
    [SwaggerResponse(StatusCodes.Status200OK, "Payment status updated")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Payment not found")]
    public async Task<IActionResult> Patch(int id, [FromBody] PatchPaymentCommand command)
    {
        var updated = await paymentCommandService.PatchAsync(id, command.Status);
        if (!updated) return NotFound(new { message = $"Payment {id} not found." });

        return Ok(new { message = $"Payment {id} updated successfully." });
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a payment",
        Description = "Deletes a specific payment record.",
        OperationId = "DeletePayment")]
    [SwaggerResponse(StatusCodes.Status200OK, "Payment deleted successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Payment not found")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await paymentCommandService.DeleteAsync(new DeletePaymentCommand { Id = id });
        if (!deleted) return NotFound(new { message = $"Payment {id} not found." });

        return Ok(new { message = $"Payment {id} deleted successfully." });
    }

    [HttpGet("pending/{ownerId}")]
    [SwaggerOperation(
        Summary = "Get pending payments by user",
        Description = "Retrieves all pending payments for a given user ID.",
        OperationId = "GetPendingPaymentsByUser")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of pending payments")]
    public async Task<IActionResult> GetPendingByUser(string ownerId)
    {
        var results = await paymentQueryService.GetPendingPaymentsByUserIdAsync(ownerId);
        return Ok(results);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get payment by ID",
        Description = "Retrieves a specific payment by its ID.",
        OperationId = "GetPaymentById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns the payment")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Payment not found")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await paymentQueryService.GetByIdAsync(id);
        return result != null ? Ok(result) : NotFound();
    }
}
