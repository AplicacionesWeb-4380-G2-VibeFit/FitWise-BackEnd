namespace UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;

public class ReviewComment
{
    public int Id { get; private set; }
    public int ReviewId { get; private set; }
    public string UserId { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public ReviewComment()
    {
        UserId = string.Empty;
        Content = string.Empty;
        CreatedAt = DateTime.UtcNow;
    }

    public ReviewComment(int reviewId, string userId, string content)
    {
        ReviewId = reviewId;
        UserId = userId;
        Content = content;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string content)
    {
        Content = content;
    }
}