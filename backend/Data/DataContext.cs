using backend.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace backend.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Entities.Type> Types { get; set; }
        public DbSet<StoreType> StoreTypes { get; set; }
        public DbSet<Design> Design { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<StoreLanguage> StoreLanguages { get; set; }
        public DbSet<DesignModel> DesignModel { get; set; } = null!;
        public DbSet<Schedule> Schedules { get; set; } = null!;
        public DbSet<SuggestProduct> SuggestProducts { get; set; } = null!;
        public DbSet<Promo> Promos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            //////
            ///
            modelBuilder.Entity<StoreType>()
                .HasKey(st => new { st.StoreId, st.TypeId });

            modelBuilder.Entity<StoreType>()
                .HasOne(st => st.Store)
                .WithMany(s => s.StoreTypes)
                .HasForeignKey(st => st.StoreId);

            modelBuilder.Entity<StoreType>()
                .HasOne(st => st.Type)
                .WithMany(t => t.StoreTypes)
                .HasForeignKey(t => t.TypeId);

            
            modelBuilder.Entity<StoreLanguage>()
                .HasKey(sl => new { sl.StoreId, sl.LanguageId });

            modelBuilder.Entity<StoreLanguage>()
                .HasOne(sl => sl.Store)
                .WithMany(s => s.StoreLanguages)
                .HasForeignKey(sl => sl.StoreId);

            modelBuilder.Entity<StoreLanguage>()
                .HasOne(sl => sl.Language)
                .WithMany(l => l.StoreLanguages)
                .HasForeignKey(l => l.LanguageId);


            //modelBuilder.Entity<Role>().HasData(
            //    new Role { Id = 1, Name = "Admin" },
            //    new Role { Id = 2, Name = "User" }
            //);

            //modelBuilder.Entity<Permission>().HasData(
            //    new Permission { Id = 1, Name = "View" },
            //    new Permission { Id = 2, Name = "Edit" }
            //);

            //modelBuilder.Entity<RolePermission>().HasData(
            //    new RolePermission { RoleId = 1, PermissionId = 1 }, // Admin - View
            //    new RolePermission { RoleId = 1, PermissionId = 2 }, // Admin - Edit
            //    new RolePermission { RoleId = 2, PermissionId = 1 }  // User - View
            //);
        }

    }

    
}