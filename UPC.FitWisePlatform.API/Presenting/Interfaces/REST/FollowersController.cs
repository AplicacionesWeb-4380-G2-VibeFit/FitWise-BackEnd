using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UPC.FitWisePlatform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Presenting.Domain.Services;
using UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Resources;
using UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Transform;

namespace UPC.FitWisePlatform.API.Presenting.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available operations for managing followers in the FitWise Platform.")]
public class FollowersController(IFollowerCommandService  followerCommandService,
    IFollowerQueryService  followerQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all followers",
        Description = "Retrieves all followers available in the FitWise Platform.",
        OperationId = "GetAllFollowers")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns Followers", typeof(IEnumerable<FollowerResource>))]
    public async Task<IActionResult> GetAllFollowers([FromQuery] int? followerUserId)
    {
        var followers = await followerQueryService.Handle(new GetAllFollowerQuery(followerUserId));
        var followerResources = followers.Select(FollowerResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(followerResources);
    }
    
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get follower by Id",
        Description = "Retrieves a follower available in the FitWise Platform.",
        OperationId = "GetFollowerById")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns a follower", typeof(FollowerResource))]
    public async Task<IActionResult> GetFollowerById(int id)
    {
        var getFollowerByIdQuery = new GetFollowerByIdQuery(id);
        var follower = await followerQueryService.Handle(getFollowerByIdQuery);
        if (follower is null)
        {
            return NotFound();
        }
        var followerResource = FollowerResourceFromEntityAssembler.ToResourceFromEntity(follower);
        return Ok(followerResource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new follower",
        Description = "Create a new follower",
        OperationId = "CreateFollower")
    ]
    [SwaggerResponse(StatusCodes.Status201Created, "The follower was created", 
        typeof(FollowerResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The follower could not be created")]
    public async Task<IActionResult> CreateFollower([FromBody] CreateFollowerResource resource)
    {
        var createdFollowerCommand = CreateFollowerCommandFromResourceAssembler.ToCommandFromResource(resource);
        var follower = await followerCommandService.Handle(createdFollowerCommand);
        if (follower is null) return BadRequest();
        var followerResource = FollowerResourceFromEntityAssembler.ToResourceFromEntity(follower);
        return CreatedAtAction(nameof(GetFollowerById), 
            new { id = followerResource.Id }, followerResource);
    }
    
    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Delete a follower by Id",
        Description = "Deletes a follower from the FitWise Platform.",
        OperationId = "DeleteFollower")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "The follower was successfully deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The follower was not found")]
    public async Task<IActionResult> DeleteFollower(int id)
    {
        var deletedFollowerCommand = new DeleteFollowerCommand(id);
        var result = await followerCommandService.Handle(deletedFollowerCommand);
        if (!result) return NotFound();
        return Ok($"Follower with ID {id} was deleted successfully.");
    }
}