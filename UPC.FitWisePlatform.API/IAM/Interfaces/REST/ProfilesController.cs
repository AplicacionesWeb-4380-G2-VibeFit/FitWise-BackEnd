using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UPC.FitWisePlatform.API.IAM.Domain.Model.Queries;
using UPC.FitWisePlatform.API.IAM.Domain.Services;
using UPC.FitWisePlatform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using UPC.FitWisePlatform.API.IAM.Interfaces.REST.Resources;
using UPC.FitWisePlatform.API.IAM.Interfaces.REST.Transform;

namespace UPC.FitWisePlatform.API.IAM.Interfaces.REST;

/**
 * <summary>
 *     The user's controller
 * </summary>
 * <remarks>
 *     This class is used to handle user requests
 * </remarks>
 */
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User endpoints")]
public class ProfilesController(IProfileQueryService profileQueryService) : ControllerBase
{
    /**
     * <summary>
     *     Get user by id endpoint. It allows to get a user by id
     * </summary>
     * <param name="id">The user id</param>
     * <returns>The user resource</returns>
     */
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a user by its id",
        Description = "Get a user by its id",
        OperationId = "GetUserById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was found", typeof(ProfileResource))]
    public async Task<IActionResult> GetUserById(int id)
    {
        var getUserByIdQuery = new GetProfileByIdQuery(id);
        var user = await profileQueryService.Handle(getUserByIdQuery);
        var userResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(user!);
        return Ok(userResource);
    }

    /**
     * <summary>
     *     Get all users' endpoint. It allows getting all users
     * </summary>
     * <returns>The user resources</returns>
     */
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all users",
        Description = "Get all users",
        OperationId = "GetAllUsers")]
    [SwaggerResponse(StatusCodes.Status200OK, "The users were found", typeof(IEnumerable<ProfileResource>))]
    public async Task<IActionResult> GetAllUsers()
    {
        var getAllUsersQuery = new GetAllProfilesQuery();
        var users = await profileQueryService.Handle(getAllUsersQuery);
        var userResources = users.Select(ProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }
}