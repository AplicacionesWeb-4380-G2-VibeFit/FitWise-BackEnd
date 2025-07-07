using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Organizing.Infrastructure.Persistence.EFC.Configuration.Extensions;
using UPC.FitWisePlatform.API.Presenting.Infrastructure.Persistence.EFC.Configuration.Extensions;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Publishing.Infrastructure.Persistence.EFC.Configuration.Extensions;
using UPC.FitWisePlatform.API.Reviewing.Infrastructure.Persistence.EFC.Configuration.Extensions;
using UPC.FitWisePlatform.API.Selling.Infrastructure.Persistence.EFC.Configuration.Extensions;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

  protected override void OnModelCreating(ModelBuilder builder)
  {
      base.OnModelCreating(builder);

      // Publishing Context Configuration
      builder.ApplyPublishingConfiguration();

      // Naming convention
      builder.UseSnakeCaseNamingConvention();

      // Reviewing Context Configuration
      builder.ApplyReviewingConfiguration();

      // Selling Context Configuration
      builder.ApplySellingConfiguration();

      // Organizing Context Configuration
      builder.ApplyScheduleConfiguration();
      
      // Presenting Context Configuration
      builder.ApplyPresentingConfiguration();
  }
  
}