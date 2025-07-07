using Mysqlx.Crud;

namespace UPC.FitWisePlatform.API.Organizing.Domain.Model.Aggregate;

public partial class Schedule {
    public int Id { get; }
    
    public int UserId { get; set; }
    
    public int HealthPlanId { get; set; }
    
    public DateTime Date { get; set; }


    public Schedule() { }

    public Schedule(int userId, int healthPlanId, DateTime date)
    {
        this.UserId = userId;
        this.HealthPlanId = healthPlanId;
        this.Date = date;
    }

    public void Update(DateTime date)
    {
        this.Date = date;
    }
}