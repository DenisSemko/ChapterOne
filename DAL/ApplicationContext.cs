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
        public DbSet<BooksTypes> BooksTypes {  get; set; }
        public DbSet<Genre> Genre {  get; set; }
        public DbSet<Book> Book {  get; set; }
        public DbSet<BookFile> BookFile {  get; set; }
        public DbSet<BookImage> BookImage {  get; set; }
        public DbSet<Combination> Combination {  get; set; }
        public DbSet<Category> Category {  get; set; }
        public DbSet<BookCollection> BookCollection {  get; set; }
        public DbSet<Collection> Collection {  get; set; }
        public DbSet<Rate> Rate {  get; set; }
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

            //Combination-User
            modelBuilder.Entity<Combination>()
                .HasOne(s => s.Reader)
                .WithMany(a => a.Combinations);

            modelBuilder.Entity<User>()
                .HasMany(s => s.Combinations)
                .WithOne(a => a.Reader);

            //BooksTypes-Book
            modelBuilder.Entity<BooksTypes>()
                 .HasOne(s => s.Book)
                 .WithMany(a => a.BooksTypes);

            modelBuilder.Entity<Book>()
                .HasMany(s => s.BooksTypes)
                .WithOne(a => a.Book);

            //BooksTypes-Type
            modelBuilder.Entity<BooksTypes>()
                .HasOne(s => s.Type)
                .WithMany(a => a.BooksTypes);

            modelBuilder.Entity<Type>()
                .HasMany(s => s.BooksTypes)
                .WithOne(a => a.Type);

            //BookCollection - Book
            modelBuilder.Entity<BookCollection>()
                 .HasOne(s => s.Book)
                 .WithMany(a => a.BookCollections);

            modelBuilder.Entity<Book>()
                .HasMany(s => s.BookCollections)
                .WithOne(a => a.Book);

            //BookCollection - Collection
            modelBuilder.Entity<BookCollection>()
                 .HasOne(s => s.Collection)
                 .WithMany(a => a.BookCollections);

            modelBuilder.Entity<Collection>()
                .HasMany(s => s.BookCollections)
                .WithOne(a => a.Collection);

            //Collection - User
            modelBuilder.Entity<Collection>()
               .HasIndex(b => b.Name)
               .IsUnique();

            modelBuilder.Entity<Collection>()
                 .HasOne(s => s.User)
                 .WithMany(a => a.Collections);

            modelBuilder.Entity<User>()
                .HasMany(s => s.Collections)
                .WithOne(a => a.User);

            //Collection - Category
            modelBuilder.Entity<Category>()
                .HasIndex(b => b.Name)
                .IsUnique();

            modelBuilder.Entity<Collection>()
                 .HasOne(s => s.Category)
                 .WithMany(a => a.Collections)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasMany(s => s.Collections)
                .WithOne(a => a.Category);


            //Rate - Book
            modelBuilder.Entity<Rate>()
                 .HasOne(s => s.Book)
                 .WithMany(a => a.BookRates);

            modelBuilder.Entity<Book>()
                .HasMany(s => s.BookRates)
                .WithOne(a => a.Book);

            //Rate - User
            modelBuilder.Entity<Rate>()
                 .HasOne(s => s.User)
                 .WithMany(a => a.BookRates);

            modelBuilder.Entity<User>()
                .HasMany(s => s.BookRates)
                .WithOne(a => a.User);

            //Category - User
            modelBuilder.Entity<Category>()
                 .HasOne(s => s.User)
                 .WithMany(a => a.Categories);

            modelBuilder.Entity<User>()
                .HasMany(s => s.Categories)
                .WithOne(a => a.User);


        }
    }
}
