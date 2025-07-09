namespace UPC.FitWisePlatform.API.IAM.Domain.Model.Queries;

/**
 * <summary>
 *     The get all users query
 * </summary>
 * <remarks>
 *     This query object includes the user id to search
 * </remarks>
 */
public record GetProfileByIdQuery(int Id);