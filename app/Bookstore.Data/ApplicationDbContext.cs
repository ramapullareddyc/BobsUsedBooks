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
            // Table mappings with schema
            modelBuilder.Entity<Address>().ToTable("Address", "dbo");
            modelBuilder.Entity<Book>().ToTable("Book", "dbo");
            modelBuilder.Entity<Customer>().ToTable("Customer", "dbo");
            modelBuilder.Entity<Order>().ToTable("Order", "dbo");
            modelBuilder.Entity<ShoppingCart>().ToTable("ShoppingCart", "dbo");
            modelBuilder.Entity<ShoppingCartItem>().ToTable("ShoppingCartItem", "dbo");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItem", "dbo");
            modelBuilder.Entity<Offer>().ToTable("Offer", "dbo");
            modelBuilder.Entity<ReferenceDataItem>().ToTable("ReferenceData", "dbo");

            // Address column mappings
            modelBuilder.Entity<Address>().Property(e => e.AddressLine1).HasColumnName("AddressLine1");
            modelBuilder.Entity<Address>().Property(e => e.AddressLine2).HasColumnName("AddressLine2");
            modelBuilder.Entity<Address>().Property(e => e.City).HasColumnName("City");
            modelBuilder.Entity<Address>().Property(e => e.State).HasColumnName("State");
            modelBuilder.Entity<Address>().Property(e => e.Country).HasColumnName("Country");
            modelBuilder.Entity<Address>().Property(e => e.ZipCode).HasColumnName("ZipCode");
            modelBuilder.Entity<Address>().Property(e => e.CustomerId).HasColumnName("CustomerId");
            modelBuilder.Entity<Address>().Property(e => e.IsActive).HasColumnName("IsActive");
            modelBuilder.Entity<Address>().Property(e => e.Id).HasColumnName("Id");
            modelBuilder.Entity<Address>().Property(e => e.CreatedBy).HasColumnName("CreatedBy");
            modelBuilder.Entity<Address>().Property(e => e.CreatedOn).HasColumnName("CreatedOn");
            modelBuilder.Entity<Address>().Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");

            // Book column mappings
            modelBuilder.Entity<Book>().Property(e => e.Name).HasColumnName("Name");
            modelBuilder.Entity<Book>().Property(e => e.Author).HasColumnName("Author");
            modelBuilder.Entity<Book>().Property(e => e.Year).HasColumnName("Year");
            modelBuilder.Entity<Book>().Property(e => e.ISBN).HasColumnName("ISBN");
            modelBuilder.Entity<Book>().Property(e => e.PublisherId).HasColumnName("PublisherId");
            modelBuilder.Entity<Book>().Property(e => e.BookTypeId).HasColumnName("BookTypeId");
            modelBuilder.Entity<Book>().Property(e => e.GenreId).HasColumnName("GenreId");
            modelBuilder.Entity<Book>().Property(e => e.ConditionId).HasColumnName("ConditionId");
            modelBuilder.Entity<Book>().Property(e => e.CoverImageUrl).HasColumnName("CoverImageUrl");
            modelBuilder.Entity<Book>().Property(e => e.Summary).HasColumnName("Summary");
            modelBuilder.Entity<Book>().Property(e => e.Price).HasColumnName("Price");
            modelBuilder.Entity<Book>().Property(e => e.Quantity).HasColumnName("Quantity");
            modelBuilder.Entity<Book>().Property(e => e.Id).HasColumnName("Id");
            modelBuilder.Entity<Book>().Property(e => e.CreatedBy).HasColumnName("CreatedBy");
            modelBuilder.Entity<Book>().Property(e => e.CreatedOn).HasColumnName("CreatedOn");
            modelBuilder.Entity<Book>().Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");

            // Customer column mappings
            modelBuilder.Entity<Customer>().Property(e => e.Sub).HasColumnName("Sub");
            modelBuilder.Entity<Customer>().Property(e => e.Username).HasColumnName("Username");
            modelBuilder.Entity<Customer>().Property(e => e.FirstName).HasColumnName("FirstName");
            modelBuilder.Entity<Customer>().Property(e => e.LastName).HasColumnName("LastName");
            modelBuilder.Entity<Customer>().Property(e => e.Email).HasColumnName("Email");
            modelBuilder.Entity<Customer>().Property(e => e.DateOfBirth).HasColumnName("DateOfBirth");
            modelBuilder.Entity<Customer>().Property(e => e.Phone).HasColumnName("Phone");
            modelBuilder.Entity<Customer>().Property(e => e.Id).HasColumnName("Id");
            modelBuilder.Entity<Customer>().Property(e => e.CreatedBy).HasColumnName("CreatedBy");
            modelBuilder.Entity<Customer>().Property(e => e.CreatedOn).HasColumnName("CreatedOn");
            modelBuilder.Entity<Customer>().Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");

            // Order column mappings
            modelBuilder.Entity<Order>().Property(e => e.CustomerId).HasColumnName("CustomerId");
            modelBuilder.Entity<Order>().Property(e => e.AddressId).HasColumnName("AddressId");
            modelBuilder.Entity<Order>().Property(e => e.DeliveryDate).HasColumnName("DeliveryDate");
            modelBuilder.Entity<Order>().Property(e => e.OrderStatus).HasColumnName("OrderStatus");
            modelBuilder.Entity<Order>().Property(e => e.Id).HasColumnName("Id");
            modelBuilder.Entity<Order>().Property(e => e.CreatedBy).HasColumnName("CreatedBy");
            modelBuilder.Entity<Order>().Property(e => e.CreatedOn).HasColumnName("CreatedOn");
            modelBuilder.Entity<Order>().Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");

            // ShoppingCart column mappings
            modelBuilder.Entity<ShoppingCart>().Property(e => e.CorrelationId).HasColumnName("CorrelationId");
            modelBuilder.Entity<ShoppingCart>().Property(e => e.Id).HasColumnName("Id");
            modelBuilder.Entity<ShoppingCart>().Property(e => e.CreatedBy).HasColumnName("CreatedBy");
            modelBuilder.Entity<ShoppingCart>().Property(e => e.CreatedOn).HasColumnName("CreatedOn");
            modelBuilder.Entity<ShoppingCart>().Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");

            // ShoppingCartItem column mappings
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.ShoppingCartId).HasColumnName("ShoppingCartId");
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.BookId).HasColumnName("BookId");
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.Quantity).HasColumnName("Quantity");
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.WantToBuy).HasColumnName("WantToBuy");
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.Id).HasColumnName("Id");
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.CreatedBy).HasColumnName("CreatedBy");
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.CreatedOn).HasColumnName("CreatedOn");
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");

            // OrderItem column mappings
            modelBuilder.Entity<OrderItem>().Property(e => e.OrderId).HasColumnName("OrderId");
            modelBuilder.Entity<OrderItem>().Property(e => e.BookId).HasColumnName("BookId");
            modelBuilder.Entity<OrderItem>().Property(e => e.Quantity).HasColumnName("Quantity");
            modelBuilder.Entity<OrderItem>().Property(e => e.Id).HasColumnName("Id");
            modelBuilder.Entity<OrderItem>().Property(e => e.CreatedBy).HasColumnName("CreatedBy");
            modelBuilder.Entity<OrderItem>().Property(e => e.CreatedOn).HasColumnName("CreatedOn");
            modelBuilder.Entity<OrderItem>().Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");

            // Offer column mappings
            modelBuilder.Entity<Offer>().Property(e => e.Author).HasColumnName("Author");
            modelBuilder.Entity<Offer>().Property(e => e.ISBN).HasColumnName("ISBN");
            modelBuilder.Entity<Offer>().Property(e => e.BookName).HasColumnName("BookName");
            modelBuilder.Entity<Offer>().Property(e => e.FrontUrl).HasColumnName("FrontUrl");
            modelBuilder.Entity<Offer>().Property(e => e.GenreId).HasColumnName("GenreId");
            modelBuilder.Entity<Offer>().Property(e => e.ConditionId).HasColumnName("ConditionId");
            modelBuilder.Entity<Offer>().Property(e => e.PublisherId).HasColumnName("PublisherId");
            modelBuilder.Entity<Offer>().Property(e => e.BookTypeId).HasColumnName("BookTypeId");
            modelBuilder.Entity<Offer>().Property(e => e.Summary).HasColumnName("Summary");
            modelBuilder.Entity<Offer>().Property(e => e.OfferStatus).HasColumnName("OfferStatus");
            modelBuilder.Entity<Offer>().Property(e => e.Comment).HasColumnName("Comment");
            modelBuilder.Entity<Offer>().Property(e => e.CustomerId).HasColumnName("CustomerId");
            modelBuilder.Entity<Offer>().Property(e => e.BookPrice).HasColumnName("BookPrice");
            modelBuilder.Entity<Offer>().Property(e => e.Id).HasColumnName("Id");
            modelBuilder.Entity<Offer>().Property(e => e.CreatedBy).HasColumnName("CreatedBy");
            modelBuilder.Entity<Offer>().Property(e => e.CreatedOn).HasColumnName("CreatedOn");
            modelBuilder.Entity<Offer>().Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");

            // ReferenceDataItem column mappings
            modelBuilder.Entity<ReferenceDataItem>().Property(e => e.DataType).HasColumnName("DataType");
            modelBuilder.Entity<ReferenceDataItem>().Property(e => e.Text).HasColumnName("Text");
            modelBuilder.Entity<ReferenceDataItem>().Property(e => e.Id).HasColumnName("Id");
            modelBuilder.Entity<ReferenceDataItem>().Property(e => e.CreatedBy).HasColumnName("CreatedBy");
            modelBuilder.Entity<ReferenceDataItem>().Property(e => e.CreatedOn).HasColumnName("CreatedOn");
            modelBuilder.Entity<ReferenceDataItem>().Property(e => e.UpdatedOn).HasColumnName("UpdatedOn");

            // Boolean property conversions for PostgreSQL
            modelBuilder.Entity<Address>().Property(e => e.IsActive).HasConversion<int>();
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.WantToBuy).HasConversion<int>();

            // Existing relationships and constraints
            modelBuilder.Entity<Customer>().HasIndex(x => x.Sub).IsUnique();

            modelBuilder.Entity<Book>().HasOne(x => x.Publisher).WithMany().HasForeignKey(x => x.PublisherId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Book>().HasOne(x => x.BookType).WithMany().HasForeignKey(x => x.BookTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Book>().HasOne(x => x.Genre).WithMany().HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Book>().HasOne(x => x.Condition).WithMany().HasForeignKey(x => x.ConditionId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Offer>().HasOne(x => x.Publisher).WithMany().HasForeignKey(x => x.PublisherId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Offer>().HasOne(x => x.BookType).WithMany().HasForeignKey(x => x.BookTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Offer>().HasOne(x => x.Genre).WithMany().HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Offer>().HasOne(x => x.Condition).WithMany().HasForeignKey(x => x.ConditionId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>().HasOne(x => x.Customer).WithMany().OnDelete(DeleteBehavior.Restrict);

            PopulateDatabase(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}