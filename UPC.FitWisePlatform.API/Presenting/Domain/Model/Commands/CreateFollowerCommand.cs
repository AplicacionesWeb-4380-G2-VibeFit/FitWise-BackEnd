namespace UPC.FitWisePlatform.API.Presenting.Domain.Model.Commands;

public record CreateFollowerCommand(
    int FollowerUserId, 
    int FollowedUserId);