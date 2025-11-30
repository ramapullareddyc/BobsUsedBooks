using System;
using Bookstore.Domain.Addresses;
using Bookstore.Domain.Books;
using Bookstore.Domain.Carts;
using Bookstore.Domain.Customers;
using Bookstore.Domain.Offers;
using Bookstore.Domain.Orders;
using Bookstore.Domain.ReferenceData;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        static ApplicationDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Address> Address { get; set; }

        public DbSet<Book> Book { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<ShoppingCart> ShoppingCart { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }

        public DbSet<OrderItem> OrderItem { get; set; }

        public DbSet<Offer> Offer { get; set; }

        public DbSet<ReferenceDataItem> ReferenceData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Address entity configuration
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address", "dbo");
                entity.Property(e => e.IsActive).HasConversion<int>();
            });

            // Book entity configuration
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book", "dbo");
                entity.HasOne(x => x.Publisher).WithMany().HasForeignKey(x => x.PublisherId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(x => x.BookType).WithMany().HasForeignKey(x => x.BookTypeId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(x => x.Genre).WithMany().HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(x => x.Condition).WithMany().HasForeignKey(x => x.ConditionId).OnDelete(DeleteBehavior.Restrict);
            });

            // Customer entity configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "dbo");
                entity.HasIndex(x => x.Sub).IsUnique();
            });

            // Order entity configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order", "dbo");
                entity.HasOne(x => x.Customer).WithMany().OnDelete(DeleteBehavior.Restrict);
            });

            // Offer entity configuration
            modelBuilder.Entity<Offer>(entity =>
            {
                entity.ToTable("Offer", "dbo");
                entity.HasOne(x => x.Publisher).WithMany().HasForeignKey(x => x.PublisherId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(x => x.BookType).WithMany().HasForeignKey(x => x.BookTypeId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(x => x.Genre).WithMany().HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(x => x.Condition).WithMany().HasForeignKey(x => x.ConditionId).OnDelete(DeleteBehavior.Restrict);
            });

            // ShoppingCart entity configuration
            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.ToTable("ShoppingCart", "dbo");
            });

            // ShoppingCartItem entity configuration
            modelBuilder.Entity<ShoppingCartItem>(entity =>
            {
                entity.ToTable("ShoppingCartItem", "dbo");
                entity.Property(e => e.WantToBuy).HasConversion<int>();
            });

            // OrderItem entity configuration
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem", "dbo");
            });

            // ReferenceDataItem entity configuration
            modelBuilder.Entity<ReferenceDataItem>(entity =>
            {
                entity.ToTable("ReferenceData", "dbo");
            });

            PopulateDatabase(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
