using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Models;

namespace RealEstateAPI.Data
{
    public class RealEstateAPIContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public RealEstateAPIContext (DbContextOptions<RealEstateAPIContext> options)
            : base(options)
        {
        }

        public DbSet<RealEstateAPI.Models.AgencyCompany> AgencyCompany { get; set; }
        public DbSet<RealEstateAPI.Models.RealEstate> RealEstate { get; set; }
        public DbSet<RealEstateAPI.Models.News> News { get; set; }
        public DbSet<RealEstateAPI.Models.Plan> Plan { get; set; }
        public DbSet<RealEstateAPI.Models.Profile> Profile { get; set; }
        public DbSet<RealEstateAPI.Models.Seller> Seller { get; set; }
        public DbSet<RealEstateAPI.Models.UserProfile> UserProfile { get; set; }
        public DbSet<RealEstateAPI.Models.Price> Price { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserProfile>()
                .HasKey(up => new { up.UserId, up.ProfileId });

            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.User)
                .WithMany(u => u.FollowedProfiles)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.Profile)
                .WithMany(p => p.Followers)
                .HasForeignKey(up => up.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Profile>()
                .HasOne(p => p.AppUser)
                .WithOne(u => u.Profile);
        }
    }
}
