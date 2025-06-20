using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Publishing.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyPublishingConfiguration(this ModelBuilder builder)
    {
        builder.Entity<HealthPlan>().HasKey(hp => hp.Id);
        builder.Entity<HealthPlan>().Property(hp => hp.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<HealthPlan>().Property(hp => hp.Name).IsRequired().HasMaxLength(100);
        builder.Entity<HealthPlan>().Property(hp => hp.Objective).IsRequired().HasMaxLength(150);
        builder.Entity<HealthPlan>().OwnsOne(hp => hp.Price,
            p =>
            {
                p.WithOwner().HasForeignKey("Id");
                p.Property(hp => hp.Amount).HasColumnName("PriceAmount");
                p.Property(hp => hp.Currency).HasColumnName("PriceCurrency");
            });
        builder.Entity<HealthPlan>().OwnsOne(hp => hp.Duration,
            d =>
            {
                d.WithOwner().HasForeignKey("Id");
                d.Property(hp => hp.Value).HasColumnName("DurationValue");
                d.Property(hp => hp.Unit).HasColumnName("DurationUnit");
            });
        builder.Entity<HealthPlan>().Property(hp => hp.Description).IsRequired().HasMaxLength(200);
        builder.Entity<HealthPlan>().Property(hp => hp.CreatorId).IsRequired();

        builder.Entity<Meal>().HasKey(m => m.Id);
        builder.Entity<Meal>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Meal>().Property(m => m.HealthPlanId).IsRequired(); 
        builder.Entity<Meal>().Property(m => m.Image).IsRequired();

        builder.Entity<Exercise>().HasKey(e => e.Id);
        builder.Entity<Exercise>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Exercise>().Property(e => e.HealthPlanId).IsRequired(); 
        builder.Entity<Exercise>().Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Exercise>().Property(e => e.Description).IsRequired().HasMaxLength(200);
        builder.Entity<Exercise>().Property(e => e.Image).IsRequired();
        
        builder.Entity<Instruction>().HasKey(it => it.Id);
        builder.Entity<Instruction>().Property(it => it.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Instruction>().Property(it => it.Position).IsRequired();
        builder.Entity<Instruction>().Property(it => it.Description).IsRequired().HasMaxLength(150);
        builder.Entity<Instruction>().Property(it => it.MealId).IsRequired();
        
        builder.Entity<Ingredient>().HasKey(ig => ig.Id);
        builder.Entity<Ingredient>().Property(ig => ig.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Ingredient>().Property(ig => ig.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Ingredient>().Property(ig => ig.Description).IsRequired().HasMaxLength(200);
        builder.Entity<Ingredient>().Property(ig => ig.MealId).IsRequired();
        
        builder.Entity<HealthPlan>()
            .HasMany(hp => hp.Meals) 
            .WithOne(m => m.HealthPlan)
            .HasForeignKey(m => m.HealthPlanId)
            .IsRequired(); 
        builder.Entity<HealthPlan>()
            .HasMany(hp => hp.Exercises) 
            .WithOne(e => e.HealthPlan)
            .HasForeignKey(e => e.HealthPlanId)
            .IsRequired(); 
        builder.Entity<Meal>()
            .HasMany(m => m.Instructions) 
            .WithOne(it => it.Meal)
            .HasForeignKey(it => it.MealId)
            .IsRequired(); 
        builder.Entity<Meal>()
            .HasMany(m => m.Ingredients) 
            .WithOne(ig => ig.Meal)
            .HasForeignKey(ig => ig.MealId)
            .IsRequired();  
    }
}