namespace UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Resources;

public record CreateFollowerResource(
    int FollowerUserId,
    int FollowedUserId);