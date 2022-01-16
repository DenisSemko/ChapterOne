using CIL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Subscription> Subscription {  get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //User-Subscription
            modelBuilder.Entity<User>()
                .HasOne(s => s.Subscription)
                .WithMany(a => a.UserSubscription);

            modelBuilder.Entity<Subscription>()
                .HasMany(s => s.UserSubscription)
                .WithOne(a => a.Subscription);
        }
    }
}
