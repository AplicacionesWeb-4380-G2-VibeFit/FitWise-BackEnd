using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Selling.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySellingConfiguration(this ModelBuilder builder)
    {
        // ========== Payment ==========
        builder.Entity<Payment>().ToTable("Payments");
        builder.Entity<Payment>().HasKey(p => p.Id);
        builder.Entity<Payment>().Property(p => p.Id).ValueGeneratedNever(); 
        builder.Entity<Payment>().Property(p => p.OwnerId).IsRequired();
        builder.Entity<Payment>().Property(p => p.PlanId).IsRequired();
        builder.Entity<Payment>().Property(p => p.Amount).IsRequired();
        builder.Entity<Payment>().Property(p => p.Currency).IsRequired();
        builder.Entity<Payment>().Property(p => p.Method).IsRequired();
        builder.Entity<Payment>().Property(p => p.Status).IsRequired();
        builder.Entity<Payment>().Property(p => p.PaymentDate).IsRequired();
        builder.Entity<Payment>().Property(p => p.PurchasedPlanId).IsRequired(false);

        // ========== PurchasedPlan ==========
        builder.Entity<PurchasedPlan>().ToTable("PurchasedPlans");
        builder.Entity<PurchasedPlan>().HasKey(pp => pp.Id);
        builder.Entity<PurchasedPlan>().Property(pp => pp.Id).ValueGeneratedOnAdd();
        builder.Entity<PurchasedPlan>().Property(pp => pp.OwnerId).IsRequired();
        builder.Entity<PurchasedPlan>().Property(pp => pp.PlanId).IsRequired();
        builder.Entity<PurchasedPlan>().Property(pp => pp.PurchaseDate).IsRequired();
        builder.Entity<PurchasedPlan>().Property(pp => pp.Status).IsRequired();

        // ========== PurchaseHistory ==========
        builder.Entity<PurchaseHistory>().ToTable("PurchaseHistories");
        builder.Entity<PurchaseHistory>().HasKey(ph => ph.Id);

        builder.Entity<PurchaseHistory>()
            .HasMany(ph => ph.Payments)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
