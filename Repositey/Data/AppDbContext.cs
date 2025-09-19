using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;

using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Repositey.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageFeature> PackageFeatures { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Package>()
            //    .HasMany(p => p.Features)
            //    .WithOne(f => f.Package)
            //    .HasForeignKey(f => f.PackageId);

            modelBuilder.Entity<Package>()
       .HasMany(p => p.Features)
       .WithOne(f => f.Package)
       .HasForeignKey(f => f.PackageId)
       .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
