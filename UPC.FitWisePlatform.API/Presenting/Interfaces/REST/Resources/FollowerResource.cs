namespace UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Resources;

public record FollowerResource(
    int Id,
    int FollowerUserId,
    int FollowedUserId);