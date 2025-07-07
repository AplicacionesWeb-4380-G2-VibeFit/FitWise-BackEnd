namespace UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;

public class ReviewReport
{
    public int Id { get; private set; }
    public int ReviewId { get; private set; }
    public int UserId { get; private set; }
    public string Reason { get; private set; }
    public string Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public ReviewReport()
    {
        UserId = 0;
        Reason = string.Empty;
        Status = "pending";
        CreatedAt = DateTime.UtcNow;
    }

    public ReviewReport(int reviewId, int userId, string reason)
    {
        ReviewId = reviewId;
        UserId = userId;
        Reason = reason;
        Status = "pending";
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(string status)
    {
        Status = status;
    }
}