using System;
using dbModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace dbModel
{
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
        public virtual DbSet<Staff> Staff { get; set; }

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

            // Location configuration
            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("locations");

                entity.HasKey(e => e.LocationId);

                entity.Property(e => e.LocationId).HasColumnName("location_id");
                entity.Property(e => e.CityName).HasColumnName("city_name").IsRequired();
                entity.Property(e => e.Country).HasColumnName("country").IsRequired();
                entity.Property(e => e.AdminName).HasColumnName("admin_name").IsRequired();
                entity.Property(e => e.Latitude).HasColumnName("latitude").HasColumnType("float");
                entity.Property(e => e.Longitude).HasColumnName("longitude").HasColumnType("float");
                entity.Property(e => e.RestaurantName).HasColumnName("restaurant_name").IsRequired();
            });

            // Review configuration
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.ReviewId);

                entity.Property(e => e.LocationId);
                entity.Property(e => e.ReviewScore).HasColumnType("float");
                entity.Property(e => e.ReviewCount).HasMaxLength(10);

                entity.HasOne<Location>()
                      .WithMany() // no nav property in Location
                      .HasForeignKey(r => r.LocationId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Review_Location");
            });

            // Staff configuration
            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("staff");

                entity.HasKey(e => e.StaffId);

                entity.Property(e => e.StaffId).HasColumnName("staff_id");
                entity.Property(e => e.FirstName).HasColumnName("first_name").IsRequired();
                entity.Property(e => e.LastName).HasColumnName("last_name").IsRequired();
                entity.Property(e => e.Position).HasColumnName("position").IsRequired();
                entity.Property(e => e.LocationId).HasColumnName("location_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
