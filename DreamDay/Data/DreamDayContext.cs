// Data/DreamDayContext.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DreamDay.Models;
using System;

namespace DreamDay.Data
{
    public class DreamDayContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DreamDayContext(DbContextOptions<DreamDayContext> options)
            : base(options)
        {
        }

        public DbSet<Wedding> Weddings { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<ChecklistItem> ChecklistItems { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<BudgetItem> BudgetItems { get; set; }
        public DbSet<TimelineEvent> TimelineEvents { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<WeddingVendor> WeddingVendors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure WeddingVendor
            modelBuilder.Entity<WeddingVendor>()
                .HasKey(wv => wv.Id); // Use single primary key instead of composite

            // Configure relationships
            modelBuilder.Entity<WeddingVendor>()
                .HasOne(wv => wv.Wedding)
                .WithMany(w => w.WeddingVendors)
                .HasForeignKey(wv => wv.WeddingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WeddingVendor>()
                .HasOne(wv => wv.Vendor)
                .WithMany(v => v.WeddingVendors)
                .HasForeignKey(wv => wv.VendorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Identity tables
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole<int>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
        }
    }
}