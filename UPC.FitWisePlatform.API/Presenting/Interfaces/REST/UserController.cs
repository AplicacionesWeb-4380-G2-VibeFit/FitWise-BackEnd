using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Presenting.Domain.Services;
using UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Resources;
using UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Transform;

namespace UPC.FitWisePlatform.API.Presenting.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available operations for managing users in the FitWise Platform.")]
public class UserController(IUserCommandService  userCommandService,
    IUserQueryService  userQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all users",
        Description = "Retrieves all users available in the FitWise Platform.",
        OperationId = "GetAllUsers")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns Users",
        typeof(IEnumerable<UserResource>))]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userQueryService.Handle(new GetAllUserQuery());
        var userResources = 
            users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }
    
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get user by Id",
        Description = "Retrieves a user available in the FitWise Platform.",
        OperationId = "GetUserById")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns a user", typeof(UserResource))]
    public async Task<IActionResult> GetUserById(int id)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var user = await userQueryService.Handle(getUserByIdQuery);
        if (user is null)
        {
            return NotFound();
        }
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(userResource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new user",
        Description = "Create a new user",
        OperationId = "CreateUser")
    ]
    [SwaggerResponse(StatusCodes.Status201Created, "The user was created", 
        typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The user could not be created")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserResource resource)
    {
        var createdUserCommand = CreateUserCommandFromResourceAssembler.ToCommandFromResource(resource);
        var user = await userCommandService.Handle(createdUserCommand);
        if (user is null) return BadRequest();
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return CreatedAtAction(nameof(GetUserById), 
            new { id = userResource.Id }, userResource);
    }
    
    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update user by Id and UpdateResource",
        Description = "Retrieves the updated user that is available in the FitWise Platform.",
        OperationId = "UpdateUserPlan")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns the updated user", typeof(UserResource))]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserResource resource)
    {
        var updatedUserCommand =
            UpdateUserCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var user = await userCommandService.Handle(updatedUserCommand);
        if (user is null) return NotFound();
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(userResource);
    }
    
    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Delete a user by Id",
        Description = "Deletes a user from the FitWise Platform.",
        OperationId = "DeleteUser")
    ]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was successfully deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The user was not found")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deletedUserCommand = new DeleteUserCommand(id);
        var result = await userCommandService.Handle(deletedUserCommand);
        if (!result) return NotFound();
        return Ok($"User with ID {id} was deleted successfully.");
    }


}