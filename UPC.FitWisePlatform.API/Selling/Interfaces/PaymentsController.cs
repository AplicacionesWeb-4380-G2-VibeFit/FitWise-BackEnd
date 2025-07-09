using Microsoft.AspNetCore.Mvc;
using UPC.FitWisePlatform.API.Selling.Application.Internal.CommandServices;
using UPC.FitWisePlatform.API.Selling.Application.Internal.QueryServices;
using UPC.FitWisePlatform.API.Selling.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Selling.Interfaces;
[ApiController]
[Route("api/v1/payments")]
public class PaymentsController(
    PaymentCommandService paymentCommandService,
    PaymentQueryService paymentQueryService)
    : ControllerBase
{
    [HttpPost]
    [HttpPost]
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
    public async Task<IActionResult> GetAll()
    {
        var payments = await paymentQueryService.GetAllAsync();
        return Ok(payments);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, [FromBody] PatchPaymentCommand command)
    {
        var updated = await paymentCommandService.PatchAsync(id, command.Status);
        if (!updated) return NotFound(new { message = $"Payment {id} not found." });

        return Ok(new { message = $"Payment {id} updated successfully." });
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await paymentCommandService.DeleteAsync(new DeletePaymentCommand { Id = id });
        if (!deleted) return NotFound(new { message = $"Payment {id} not found." });

        return Ok(new { message = $"Payment {id} deleted successfully." });
    }



    [HttpGet("pending/{ownerId}")]
    public async Task<IActionResult> GetPendingByUser(string ownerId)
    {
        var results = await paymentQueryService.GetPendingPaymentsByUserIdAsync(ownerId);
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await paymentQueryService.GetByIdAsync(id);
        return result != null ? Ok(result) : NotFound();
    }
}
