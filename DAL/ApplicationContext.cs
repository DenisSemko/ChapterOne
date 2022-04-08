using CIL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Type = CIL.Models.Type;

namespace DAL
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Subscription> Subscription {  get; set; }
        public DbSet<Type> Type {  get; set; }
        public DbSet<Genre> Genre {  get; set; }
        public DbSet<Book> Book {  get; set; }
        public DbSet<BookFile> BookFile {  get; set; }
        public DbSet<BookImage> BookImage {  get; set; }
        public DbSet<Combination> Combination {  get; set; }
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

            //Book-Genre
            modelBuilder.Entity<Book>()
                .HasOne(s => s.Genre)
                .WithMany(a => a.Books);

            modelBuilder.Entity<Genre>()
                .HasMany(s => s.Books)
                .WithOne(a => a.Genre);

            //Book-BookFile
            modelBuilder.Entity<Book>()
                .HasOne(s => s.File)
                .WithMany(a => a.Books);

            modelBuilder.Entity<BookFile>()
                .HasMany(s => s.Books)
                .WithOne(a => a.File);

            //Book-BookImage
            modelBuilder.Entity<Book>()
                .HasOne(s => s.Image)
                .WithMany(a => a.Books);

            modelBuilder.Entity<BookImage>()
                .HasMany(s => s.Books)
                .WithOne(a => a.Image);

            //Combination-Genre
            modelBuilder.Entity<Combination>()
                .HasOne(s => s.Genre)
                .WithMany(a => a.Combinations);

            modelBuilder.Entity<Genre>()
                .HasMany(s => s.Combinations)
                .WithOne(a => a.Genre);

            //Combination-User
            modelBuilder.Entity<Combination>()
                .HasOne(s => s.Reader)
                .WithMany(a => a.Combinations);

            modelBuilder.Entity<User>()
                .HasMany(s => s.Combinations)
                .WithOne(a => a.Reader);
        }
    }
}
