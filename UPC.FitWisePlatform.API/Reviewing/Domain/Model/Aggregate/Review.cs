using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Reviewing.Domain.Model.Aggregate;

public class Review
{
    public int Id { get; private set; }
    public string UserId { get; private set; }
    public int Score { get; private set; }
    public string Description { get; private set; }
    public int HealthPlanId { get; private set; }

    public ICollection<ReviewComment> Comments { get; private set; }
    public ICollection<ReviewReport> Reports { get; private set; }

    public Review()
    {
        UserId = string.Empty;
        Description = string.Empty;
        Comments = new List<ReviewComment>();
        Reports = new List<ReviewReport>();
    }

    public Review(string userId, int score, string description, int healthPlanId)
    {
        UserId = userId;
        Score = score;
        Description = description;
        HealthPlanId = healthPlanId;
        Comments = new List<ReviewComment>();
        Reports = new List<ReviewReport>();
    }

    public void Update(int score, string description)
    {
        Score = score;
        Description = description;
    }
}