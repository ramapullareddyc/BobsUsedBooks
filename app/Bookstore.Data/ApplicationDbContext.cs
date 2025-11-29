using System;
using Bookstore.Domain.Addresses;
using Bookstore.Domain.Books;
using Bookstore.Domain.Carts;
using Bookstore.Domain.Customers;
using Bookstore.Domain.Offers;
using Bookstore.Domain.Orders;
using Bookstore.Domain.ReferenceData;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

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
            // Table and column mappings for PostgreSQL
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address", "public");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.AddressLine1).HasColumnName("AddressLine1");
                entity.Property(e => e.AddressLine2).HasColumnName("AddressLine2");
                entity.Property(e => e.City).HasColumnName("City");
                entity.Property(e => e.State).HasColumnName("State");
                entity.Property(e => e.Country).HasColumnName("Country");
                entity.Property(e => e.ZipCode).HasColumnName("ZipCode");
                entity.Property(e => e.CustomerId).HasColumnName("CustomerId");
                entity.Property(e => e.IsActive).HasColumnName("IsActive").HasConversion<int>();
                entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
                entity.Property(e => e.CreatedOn).HasColumnName("CreatedOn");
                entity.Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book", "public");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.Author).HasColumnName("Author");
                entity.Property(e => e.Year).HasColumnName("Year");
                entity.Property(e => e.ISBN).HasColumnName("ISBN");
                entity.Property(e => e.PublisherId).HasColumnName("PublisherId");
                entity.Property(e => e.BookTypeId).HasColumnName("BookTypeId");
                entity.Property(e => e.GenreId).HasColumnName("GenreId");
                entity.Property(e => e.ConditionId).HasColumnName("ConditionId");
                entity.Property(e => e.CoverImageUrl).HasColumnName("CoverImageUrl");
                entity.Property(e => e.Summary).HasColumnName("Summary");
                entity.Property(e => e.Price).HasColumnName("Price");
                entity.Property(e => e.Quantity).HasColumnName("Quantity");
                entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
                entity.Property(e => e.CreatedOn).HasColumnName("CreatedOn");
                entity.Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");
                
                entity.HasOne(x => x.Publisher).WithMany().HasForeignKey(x => x.PublisherId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(x => x.BookType).WithMany().HasForeignKey(x => x.BookTypeId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(x => x.Genre).WithMany().HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(x => x.Condition).WithMany().HasForeignKey(x => x.ConditionId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "public");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Sub).HasColumnName("Sub");
                entity.Property(e => e.Username).HasColumnName("Username");
                entity.Property(e => e.FirstName).HasColumnName("FirstName");
                entity.Property(e => e.LastName).HasColumnName("LastName");
                entity.Property(e => e.Email).HasColumnName("Email");
                entity.Property(e => e.DateOfBirth).HasColumnName("DateOfBirth");
                entity.Property(e => e.Phone).HasColumnName("Phone");
                entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
                entity.Property(e => e.CreatedOn).HasColumnName("CreatedOn");
                entity.Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");
                
                entity.HasIndex(x => x.Sub).IsUnique();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order", "public");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.CustomerId).HasColumnName("CustomerId");
                entity.Property(e => e.AddressId).HasColumnName("AddressId");
                entity.Property(e => e.DeliveryDate).HasColumnName("DeliveryDate");
                entity.Property(e => e.OrderStatus).HasColumnName("OrderStatus");
                entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
                entity.Property(e => e.CreatedOn).HasColumnName("CreatedOn");
                entity.Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");
                
                entity.HasOne(x => x.Customer).WithMany().OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.ToTable("ShoppingCart", "public");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.CorrelationId).HasColumnName("CorrelationId");
                entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
                entity.Property(e => e.CreatedOn).HasColumnName("CreatedOn");
                entity.Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");
            });

            modelBuilder.Entity<ShoppingCartItem>(entity =>
            {
                entity.ToTable("ShoppingCartItem", "public");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.ShoppingCartId).HasColumnName("ShoppingCartId");
                entity.Property(e => e.BookId).HasColumnName("BookId");
                entity.Property(e => e.Quantity).HasColumnName("Quantity");
                entity.Property(e => e.WantToBuy).HasColumnName("WantToBuy").HasConversion<int>();
                entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
                entity.Property(e => e.CreatedOn).HasColumnName("CreatedOn");
                entity.Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem", "public");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.OrderId).HasColumnName("OrderId");
                entity.Property(e => e.BookId).HasColumnName("BookId");
                entity.Property(e => e.Quantity).HasColumnName("Quantity");
                entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
                entity.Property(e => e.CreatedOn).HasColumnName("CreatedOn");
                entity.Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.ToTable("Offer", "public");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Author).HasColumnName("Author");
                entity.Property(e => e.ISBN).HasColumnName("ISBN");
                entity.Property(e => e.BookName).HasColumnName("BookName");
                entity.Property(e => e.FrontUrl).HasColumnName("FrontUrl");
                entity.Property(e => e.GenreId).HasColumnName("GenreId");
                entity.Property(e => e.ConditionId).HasColumnName("ConditionId");
                entity.Property(e => e.PublisherId).HasColumnName("PublisherId");
                entity.Property(e => e.BookTypeId).HasColumnName("BookTypeId");
                entity.Property(e => e.Summary).HasColumnName("Summary");
                entity.Property(e => e.OfferStatus).HasColumnName("OfferStatus");
                entity.Property(e => e.Comment).HasColumnName("Comment");
                entity.Property(e => e.CustomerId).HasColumnName("CustomerId");
                entity.Property(e => e.BookPrice).HasColumnName("BookPrice");
                entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
                entity.Property(e => e.CreatedOn).HasColumnName("CreatedOn");
                entity.Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");
                
                entity.HasOne(x => x.Publisher).WithMany().HasForeignKey(x => x.PublisherId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(x => x.BookType).WithMany().HasForeignKey(x => x.BookTypeId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(x => x.Genre).WithMany().HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(x => x.Condition).WithMany().HasForeignKey(x => x.ConditionId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ReferenceDataItem>(entity =>
            {
                entity.ToTable("ReferenceData", "public");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.DataType).HasColumnName("DataType");
                entity.Property(e => e.Text).HasColumnName("Text");
                entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
                entity.Property(e => e.CreatedOn).HasColumnName("CreatedOn");
                entity.Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");
            });

            PopulateDatabase(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}