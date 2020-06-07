using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VidRental.DataAccess.DbModels;

namespace VidRental.DataAccess.DataContext
{
    public class VidContext : IdentityDbContext<User>
    {
        public VidContext()
        {

        }

        public VidContext(DbContextOptions<VidContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Rental>()
                .HasOne(r => r.Address)
                .WithMany(a => a.Rentals)
                .HasForeignKey(r => r.AddressId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Rental>()
                .HasOne(r => r.ShopUser)
                .WithMany(su => su.Rentals)
                .HasForeignKey(r => r.ShopUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Rental>()
                .HasOne(r => r.CartridgeCopy)
                .WithMany(cc => cc.Rentals)
                .HasForeignKey(r => r.CartridgeCopyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Address>()
                .HasMany(a => a.Rentals)
                .WithOne(r => r.Address)
                .HasForeignKey(r => r.AddressId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ShopUser>()
                .HasMany(su => su.Rentals)
                .WithOne(r => r.ShopUser)
                .HasForeignKey(r => r.ShopUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CartridgeCopy>()
                .HasMany(cc => cc.Rentals)
                .WithOne(r => r.CartridgeCopy)
                .HasForeignKey(r => r.CartridgeCopyId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<ShopUser> ShopUsers { get; set; }
        public DbSet<ShopEmployee> ShopEmployees { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieImage> MovieImages { get; set; }
        public DbSet<Cartridge> Cartridges { get; set; }
        public DbSet<CartridgeCopy> CartridgeCopies { get; set; }
    }
}
