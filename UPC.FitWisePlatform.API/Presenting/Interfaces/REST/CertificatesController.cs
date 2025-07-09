using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Presenting.Domain.Services;
using UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Resources;
using UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Transform;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available operations for managing certificates in the FitWise Platform.")]
public class CertificatesController(ICertificateCommandService certificateCommandService,
    ICertificateQueryService certificateQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all certificates",
        Description = "Retrieves all certificates available in the FitWise Platform.",
        OperationId = "GetAllCertificates")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns Certificates", typeof(IEnumerable<CertificateResource>))]
    public async Task<IActionResult> GetAllCertificates([FromQuery] int? userId)
    {
        var certificates = await certificateQueryService.Handle(new GetAllCertificateQuery(userId));
        var certificateResources = certificates.Select(CertificateResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(certificateResources);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get certificate by Id",
        Description = "Retrieves a certificate available in the FitWise Platform.",
        OperationId = "GetCertificateById")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns a certificate", typeof(CertificateResource))]
    public async Task<IActionResult> GetCertificateById(int id)
    {
        var getCertificateByIdQuery = new GetCertificateByIdQuery(id);
        var certificate = await certificateQueryService.Handle(getCertificateByIdQuery);
        if (certificate is null)
        {
            return NotFound();
        }
        var certificateResource = CertificateResourceFromEntityAssembler.ToResourceFromEntity(certificate);
        return Ok(certificateResource);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new certificate",
        Description = "Create a new certificate",
        OperationId = "CreateCertificate")
    ]
    [SwaggerResponse(StatusCodes.Status201Created, "The certificate was created", typeof(CertificateResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The certificate could not be created")]
    public async Task<IActionResult> CreateCertificate([FromBody] CreateCertificateResource resource)
    {
        var createdCertificateCommand = CreateCertificateCommandFromResourceAssembler.ToCommandFromResource(resource);
        var certificate = await certificateCommandService.Handle(createdCertificateCommand);
        if (certificate is null) return BadRequest();
        var certificateResource = CertificateResourceFromEntityAssembler.ToResourceFromEntity(certificate);
        return CreatedAtAction(nameof(GetCertificateById), new { id = certificateResource.Id }, certificateResource);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update certificate by Id and UpdateResource",
        Description = "Retrieves the updated certificate that is available in the FitWise Platform.",
        OperationId = "UpdateCertificate")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns the updated certificate", typeof(CertificateResource))]
    public async Task<IActionResult> UpdateCertificate(int id, [FromBody] UpdateCertificateResource resource)
    {
        var updatedCertificateCommand = UpdateCertificateCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var certificate = await certificateCommandService.Handle(updatedCertificateCommand);
        if (certificate is null) return NotFound();
        var certificateResource = CertificateResourceFromEntityAssembler.ToResourceFromEntity(certificate);
        return Ok(certificateResource);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Delete a certificate by Id",
        Description = "Deletes a certificate from the FitWise Platform.",
        OperationId = "DeleteCertificate")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "The certificate was successfully deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The certificate was not found")]
    public async Task<IActionResult> DeleteCertificate(int id)
    {
        var deletedCertificateCommand = new DeleteCertificateCommand(id);
        var result = await certificateCommandService.Handle(deletedCertificateCommand);
        if (!result) return NotFound();
        return Ok($"Certificate with ID {id} was deleted successfully.");
    }
}