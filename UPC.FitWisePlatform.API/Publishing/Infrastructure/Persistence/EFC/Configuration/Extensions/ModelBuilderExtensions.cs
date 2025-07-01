using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Publishing.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyPublishingConfiguration(this ModelBuilder builder)
    {
        // Health Plan Configuration
        builder.Entity<HealthPlan>().HasKey(hp => hp.Id);
        builder.Entity<HealthPlan>().Property(hp => hp.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<HealthPlan>().Property(hp => hp.PlanName).IsRequired().HasMaxLength(50);
        builder.Entity<HealthPlan>().Property(hp => hp.Description).HasMaxLength(300);
        builder.Entity<HealthPlan>().OwnsOne(hp => hp.Duration, pd =>
        {
            pd.WithOwner().HasForeignKey("Id");
            pd.Property(a => a.DurationValue).IsRequired();
            pd.Property(a => a.DurationType).HasConversion<string>().IsRequired();
        });
        builder.Entity<HealthPlan>().OwnsOne(hp => hp.Price, pp =>
        {
            pp.WithOwner().HasForeignKey("Id");
            pp.Property(a => a.PriceValue).IsRequired();
            pp.Property(a => a.Currency).HasConversion<string>().IsRequired();
        });
        builder.Entity<HealthPlan>().Property(hp => hp.ProfileId).IsRequired();
        
        // Navigation collections for the many-to-many relationships with Meals and Exercises
        builder.Entity<HealthPlan>()
            .HasMany(h => h.HealthPlanMeals)
            .WithOne(hpm => hpm.HealthPlan)
            .HasForeignKey(hpm => hpm.HealthPlanId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); // If HealthPlan is deleted, associated HealthPlanMeals are deleted

        builder.Entity<HealthPlan>()
            .HasMany(h => h.HealthPlanExercises)
            .WithOne(hpe => hpe.HealthPlan)
            .HasForeignKey(hpe => hpe.HealthPlanId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); // If HealthPlan is deleted, associated HealthPlanExercises are deleted
        
        // Meal Configuration
        builder.Entity<Meal>().HasKey(m => m.Id);
        builder.Entity<Meal>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Meal>().Property(m => m.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Meal>().Property(m => m.Description).IsRequired().HasMaxLength(300);
        builder.Entity<Meal>().Property(m => m.ImageUri).IsRequired();
        
        builder.Entity<Meal>()
            .HasMany(me => me.HealthPlanMeals)
            .WithOne(hpm => hpm.Meal)
            .HasForeignKey(hpm => hpm.MealId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a Meal if it's referenced by a HealthPlanMeal
        
        // Exercise Configuration
        builder.Entity<Exercise>().HasKey(e => e.Id);
        builder.Entity<Exercise>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Exercise>().Property(e => e.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Exercise>().Property(e => e.Description).IsRequired().HasMaxLength(300);
        builder.Entity<Exercise>().Property(e => e.ImageUri).IsRequired();
        
        builder.Entity<Exercise>()
            .HasMany(ex => ex.HealthPlanExercises)
            .WithOne(hpe => hpe.Exercise)
            .HasForeignKey(hpe => hpe.ExerciseId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict); // Prevent deleting an Exercise if it's referenced by a HealthPlanExercise
        
        // HealthPlanMeal Configuration
        builder.Entity<HealthPlanMeal>().HasKey(hpm => hpm.Id);
        builder.Entity<HealthPlanMeal>().Property(hpm => hpm.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<HealthPlanMeal>()
            .Property(hpm => hpm.DayOfWeek)
            .HasConversion<string>() // Store enum as string in DB for readability
            .IsRequired()
            .HasMaxLength(20); // Adjust max length based on enum string values
        builder.Entity<HealthPlanMeal>()
            .Property(hpm => hpm.MealTime)
            .HasConversion<string>() // Store enum as string in DB for readability
            .IsRequired()
            .HasMaxLength(20); // Adjust max length based on enum string values
        builder.Entity<HealthPlanMeal>().Property(hpm => hpm.Notes).HasMaxLength(200); // Notes can be nullable
        
        // Define the foreign key relationships
        builder.Entity<HealthPlanMeal>()
            .HasOne(hpm => hpm.HealthPlan)
            .WithMany(m => m.HealthPlanMeals) // Or .WithMany(hp => hp.HealthPlanMeals) if you add a collection to HealthPlan
            .HasForeignKey(hpm => hpm.HealthPlanId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); // If a HealthPlan is deleted, its associated HealthPlanMeals are also deleted
        builder.Entity<HealthPlanMeal>()
            .HasOne(hpm => hpm.Meal)
            .WithMany(m => m.HealthPlanMeals) // Or .WithMany(m => m.HealthPlanMeals) if you add a collection to Meal
            .HasForeignKey(hpm => hpm.MealId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a Meal if it's part of a HealthPlanMeal (or SetNull)
        
        // HealthPlanExercise Configuration
        builder.Entity<HealthPlanExercise>().HasKey(hpe => hpe.Id);
        builder.Entity<HealthPlanExercise>().Property(hpe => hpe.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<HealthPlanExercise>()
            .Property(hpe => hpe.DayOfWeek)
            .HasConversion<string>() // Store enum as string in DB
            .IsRequired()
            .HasMaxLength(20); // Adjust max length based on enum string values
        builder.Entity<HealthPlanExercise>().Property(hpe => hpe.Sets).IsRequired();
        builder.Entity<HealthPlanExercise>().Property(hpe => hpe.Reps).IsRequired();
        builder.Entity<HealthPlanExercise>().Property(hpe => hpe.DurationInMinutes); // Optional, so no IsRequired()
        builder.Entity<HealthPlanExercise>().Property(hpe => hpe.Instructions).HasMaxLength(200); // Instructions can be nullable
        
        // Define the foreign key relationships
        builder.Entity<HealthPlanExercise>()
            .HasOne(hpe => hpe.HealthPlan)
            .WithMany(hp => hp.HealthPlanExercises) // Or .WithMany(hp => hp.HealthPlanExercises) if you add a collection to HealthPlan
            .HasForeignKey(hpe => hpe.HealthPlanId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); // If a HealthPlan is deleted, its associated HealthPlanExercises are also deleted
        builder.Entity<HealthPlanExercise>()
            .HasOne(hpe => hpe.Exercise)
            .WithMany(hp => hp.HealthPlanExercises) // Or .WithMany(e => e.HealthPlanExercises) if you add a collection to Exercise
            .HasForeignKey(hpe => hpe.ExerciseId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict); // Prevent deleting an Exercise if it's part of a HealthPlanExercise (or SetNull)
    }
}