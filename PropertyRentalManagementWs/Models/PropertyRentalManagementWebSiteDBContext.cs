using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PropertyRentalManagementWs.Models
{
    public partial class PropertyRentalManagementWebSiteDBContext : DbContext
    {
        public PropertyRentalManagementWebSiteDBContext()
        {
        }

        public PropertyRentalManagementWebSiteDBContext(DbContextOptions<PropertyRentalManagementWebSiteDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Apartment> Apartment { get; set; }
        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<Building> Building { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<PotentialTenant> PotentialTenant { get; set; }
        public virtual DbSet<PropertyManager> PropertyManager { get; set; }
        public virtual DbSet<PropertyOwner> PropertyOwner { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("PropertyRentalManagementWebSiteDB");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BuildingId).HasColumnName("building_id");

                entity.Property(e => e.Img1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Img2)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Img3)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumberOfRooms).HasColumnName("number_of_rooms");

                entity.Property(e => e.RentPrice)
                    .HasColumnName("rent_price")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Apartment)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Apartment__build__2F10007B");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApartmentId).HasColumnName("apartment_id");

                entity.Property(e => e.DateTime)
                    .HasColumnName("date_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.PotentialTenantId).HasColumnName("potential_tenant_id");

                entity.Property(e => e.PropertyManagerId).HasColumnName("property_manager_id");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.ApartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Appointme__apart__33D4B598");

                entity.HasOne(d => d.PotentialTenant)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.PotentialTenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Appointme__poten__31EC6D26");

                entity.HasOne(d => d.PropertyManager)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.PropertyManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Appointme__prope__32E0915F");
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyManagerId).HasColumnName("property_manager_id");

                entity.HasOne(d => d.PropertyManager)
                    .WithMany(p => p.Building)
                    .HasForeignKey(d => d.PropertyManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Building__proper__2C3393D0");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("text");

                entity.Property(e => e.DateTime)
                    .HasColumnName("date_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.PotentialTenantId).HasColumnName("potential_tenant_id");

                entity.Property(e => e.PropertyManagerId).HasColumnName("property_manager_id");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasColumnName("subject")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.PotentialTenant)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.PotentialTenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__potenti__36B12243");

                entity.HasOne(d => d.PropertyManager)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.PropertyManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__propert__37A5467C");
            });

            modelBuilder.Entity<PotentialTenant>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PropertyManager>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PropertyOwner>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
