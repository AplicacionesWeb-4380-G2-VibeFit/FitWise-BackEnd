namespace UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;

public partial class Follower
{
    public int Id { get; }
    
    public int FollowerUserId { get; private set; }
    
    public int FollowedUserId { get; private set; }
    
    public Follower() { }
    
    public Follower(int followerUserId, int followedUserId)
    {
        if (followerUserId <= 0)
            throw new ArgumentOutOfRangeException(nameof(followerUserId), "Follower user ID must be greater than zero.");
        if (followedUserId <= 0)
            throw new ArgumentOutOfRangeException(nameof(followedUserId), "Followed user ID must be greater than zero.");
        
        this.FollowerUserId = followerUserId;
        this.FollowedUserId = followedUserId;
    }
}