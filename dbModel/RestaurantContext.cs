using System;
using dbModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace dbModel;

public partial class RestaurantContext : IdentityDbContext<User>
{
    public RestaurantContext()
    {
    }

    public RestaurantContext(DbContextOptions<RestaurantContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");

        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId);

            entity.Property(e => e.CityName).IsRequired();  
            entity.Property(e => e.Country).IsRequired();  
            entity.Property(e => e.AdminName).IsRequired();
            entity.Property(e => e.Latitude).HasColumnType("float");
            entity.Property(e => e.Longitude).HasColumnType("float");
            entity.Property(e => e.RestaurantName); 
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId);

            entity.Property(e => e.LocationId); 
            entity.Property(e => e.ReviewScore).HasColumnType("float");
            entity.Property(e => e.ReviewCount).HasMaxLength(10);  

            entity.HasOne<Location>()
                  .WithMany()
                  .HasForeignKey(r => r.LocationId) 
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Review_Location");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
