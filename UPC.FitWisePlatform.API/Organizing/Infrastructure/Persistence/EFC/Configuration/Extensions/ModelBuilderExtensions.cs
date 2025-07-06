using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Organizing.Domain.Model.Aggregate;

namespace UPC.FitWisePlatform.API.Organizing.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyScheduleConfiguration(this ModelBuilder builder)
    {
        //Schedule configuration
        builder.Entity<Schedule>(schedule =>
        {
            schedule.HasKey(s => s.Id);
            schedule.Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();

            schedule.Property(s => s.UserId).IsRequired();
            schedule.Property(s=>s.HealthPlanId).IsRequired();
            schedule.Property(s=>s.Date).IsRequired();
            
        });
    }
}