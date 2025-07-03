using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Reviewing.Infrastructure.Persistence.EFC.Configuration.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyReviewingConfiguration(this ModelBuilder builder)
        {
            // Review configuration
            builder.Entity<Review>(review =>
            {
                review.HasKey(r => r.Id);
                review.Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();

                review.Property(r => r.UserId).IsRequired();
                review.Property(r => r.Score).IsRequired();
                review.Property(r => r.Description).IsRequired();
                review.Property(r => r.HealthPlanId).IsRequired();

                // Navigation collections
                review.HasMany(r => r.Comments)
                      .WithOne()
                      .HasForeignKey(c => c.ReviewId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);

                review.HasMany(r => r.Reports)
                      .WithOne()
                      .HasForeignKey(rep => rep.ReviewId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ReviewComment configuration
            builder.Entity<ReviewComment>(comment =>
            {
                comment.HasKey(c => c.Id);
                comment.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();

                comment.Property(c => c.ReviewId).IsRequired();
                comment.Property(c => c.UserId).IsRequired();
                comment.Property(c => c.Content).IsRequired();
                comment.Property(c => c.CreatedAt).IsRequired();
            });

            // ReviewReport configuration
            builder.Entity<ReviewReport>(report =>
            {
                report.HasKey(r => r.Id);
                report.Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();

                report.Property(r => r.ReviewId).IsRequired();
                report.Property(r => r.UserId).IsRequired();
                report.Property(r => r.Reason).IsRequired();
                report.Property(r => r.Status).IsRequired().HasMaxLength(20);
                report.Property(r => r.CreatedAt).IsRequired();
            });
        }
    }
}
