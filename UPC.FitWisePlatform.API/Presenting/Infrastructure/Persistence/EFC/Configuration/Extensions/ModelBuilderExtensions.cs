using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Presenting.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyPresentingConfiguration(this ModelBuilder builder)
    {
        // User Configuration
        builder.Entity<User>().HasKey(us => us.Id);
        builder.Entity<User>().Property(us => us.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(us => us.FirstName).IsRequired().HasMaxLength(50);
        builder.Entity<User>().Property(us => us.LastName).IsRequired().HasMaxLength(50);
        builder.Entity<User>().OwnsOne(us => us.Email, pd =>
        {
            pd.WithOwner().HasForeignKey("Id");
            pd.Property(a => a.EmailValue).IsRequired().HasMaxLength(500);
        });
        builder.Entity<User>().OwnsOne(us => us.BirthDate, pd =>
        {
            pd.WithOwner().HasForeignKey("Id");
            pd.Property(a => a.BirthDateValue).IsRequired().HasMaxLength(15);
        });
        builder.Entity<User>()
            .Property(hpm => hpm.Gender)
            .HasConversion<string>() // Store enum as string in DB for readability
            .IsRequired()
            .HasMaxLength(10); // Adjust max length based on enum string values

        builder.Entity<User>().OwnsOne(us => us.Image, pd =>
        {
            pd.WithOwner().HasForeignKey("Id");
            pd.Property(a => a.Url).IsRequired().HasMaxLength(200);
        });
        builder.Entity<User>().Property(us => us.AboutMe).IsRequired().HasMaxLength(500);
        builder.Entity<User>()
            .Property(us => us.ProfileId)
            .IsRequired();
        builder.Entity<User>()
            .HasIndex(us => us.ProfileId)
            .IsUnique();
        
        // Relación: Un User puede tener muchos Certificados 
        builder.Entity<User>()
            .HasMany(u => u.Certificates)
            .WithOne()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        
        // Follower Configuration
        builder.Entity<Follower>().HasKey(fo => fo.Id);
        builder.Entity<Follower>().Property(fo => fo.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Follower>().Property(fo => fo.FollowerUserId).IsRequired();
        builder.Entity<Follower>().Property(fo => fo.FollowedUserId).IsRequired();
        
        // Relación: Un User puede tener muchos Followers donde es el seguidor
        builder.Entity<Follower>()
            .HasOne<User>()
            .WithMany(u => u.FollowerUsers)
            .HasForeignKey(f => f.FollowerUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relación: Un User puede tener muchos Followers donde es el seguido
        builder.Entity<Follower>()
            .HasOne<User>()
            .WithMany(u => u.FollowedUsers) // Si quieres, puedes agregar otra colección en User, por ejemplo: Followings
            .HasForeignKey(f => f.FollowedUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        
        // Certificate Configuration
        builder.Entity<Certificate>().HasKey(c => c.Id);
        builder.Entity<Certificate>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Certificate>().Property(c => c.UserId).IsRequired();
        builder.Entity<Certificate>().Property(c => c.Institution).IsRequired().HasMaxLength(100);
        builder.Entity<Certificate>().OwnsOne(c => c.DateObtained, pd =>
        {
            pd.WithOwner().HasForeignKey("Id");
            pd.Property(a => a.DateObtainedValue).IsRequired().HasMaxLength(25);
        });
        builder.Entity<Certificate>().Property(c => c.Description).IsRequired().HasMaxLength(360);
        builder.Entity<Certificate>().Property(c => c.Status)
            .HasConversion<string>() // Store enum as string in DB for readability
            .IsRequired()
            .HasMaxLength(30); // Adjust max length based on enum string values
        builder.Entity<Certificate>().OwnsOne(c => c.CertificateCode, pd =>
        {
            pd.WithOwner().HasForeignKey("Id");
            pd.Property(a => a.CodeValue).IsRequired().HasMaxLength(30);
        });
        builder.Entity<Certificate>().Property(c => c.YearsOfWork).IsRequired();
        
        
    }
}