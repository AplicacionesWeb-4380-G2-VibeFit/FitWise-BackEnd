using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.IAM.Domain.Model.Aggregates;

namespace UPC.FitWisePlatform.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        // IAM Context
        
        builder.Entity<Profile>().HasKey(u => u.Id);
        builder.Entity<Profile>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Profile>().Property(u => u.Username).IsRequired();
        builder.Entity<Profile>().Property(u => u.PasswordHash).IsRequired();
    }
}