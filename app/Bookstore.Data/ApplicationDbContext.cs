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
            // Table and column mappings for PostgreSQL
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address", "public");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.AddressLine1).HasColumnName("address_line1");
                entity.Property(e => e.AddressLine2).HasColumnName("address_line2");
                entity.Property(e => e.City).HasColumnName("city");
                entity.Property(e => e.State).HasColumnName("state");
                entity.Property(e => e.Country).HasColumnName("country");
                entity.Property(e => e.ZipCode).HasColumnName("zip_code");
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.IsActive).HasColumnName("is_active").HasConversion<int>();
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.CreatedOn).HasColumnName("created_on");
                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book", "public");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Author).HasColumnName("author");
                entity.Property(e => e.Year).HasColumnName("year");
                entity.Property(e => e.ISBN).HasColumnName("isbn");
                entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
                entity.Property(e => e.BookTypeId).HasColumnName("book_type_id");
                entity.Property(e => e.GenreId).HasColumnName("genre_id");
                entity.Property(e => e.ConditionId).HasColumnName("condition_id");
                entity.Property(e => e.CoverImageUrl).HasColumnName("cover_image_url");
                entity.Property(e => e.Summary).HasColumnName("summary");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.CreatedOn).HasColumnName("created_on");
                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer", "public");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Sub).HasColumnName("sub");
                entity.Property(e => e.Username).HasColumnName("username");
                entity.Property(e => e.FirstName).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasColumnName("last_name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
                entity.Property(e => e.Phone).HasColumnName("phone");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.CreatedOn).HasColumnName("created_on");
                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order", "public");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.AddressId).HasColumnName("address_id");
                entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date");
                entity.Property(e => e.OrderStatus).HasColumnName("order_status");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.CreatedOn).HasColumnName("created_on");
                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.ToTable("shopping_cart", "public");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CorrelationId).HasColumnName("correlation_id");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.CreatedOn).HasColumnName("created_on");
                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");
            });

            modelBuilder.Entity<ShoppingCartItem>(entity =>
            {
                entity.ToTable("shopping_cart_item", "public");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ShoppingCartId).HasColumnName("shopping_cart_id");
                entity.Property(e => e.BookId).HasColumnName("book_id");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.WantToBuy).HasColumnName("want_to_buy").HasConversion<int>();
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.CreatedOn).HasColumnName("created_on");
                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_item", "public");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.BookId).HasColumnName("book_id");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.CreatedOn).HasColumnName("created_on");
                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.ToTable("offer", "public");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Author).HasColumnName("author");
                entity.Property(e => e.ISBN).HasColumnName("isbn");
                entity.Property(e => e.BookName).HasColumnName("book_name");
                entity.Property(e => e.FrontUrl).HasColumnName("front_url");
                entity.Property(e => e.GenreId).HasColumnName("genre_id");
                entity.Property(e => e.ConditionId).HasColumnName("condition_id");
                entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
                entity.Property(e => e.BookTypeId).HasColumnName("book_type_id");
                entity.Property(e => e.Summary).HasColumnName("summary");
                entity.Property(e => e.OfferStatus).HasColumnName("offer_status");
                entity.Property(e => e.Comment).HasColumnName("comment");
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.BookPrice).HasColumnName("book_price");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.CreatedOn).HasColumnName("created_on");
                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");
            });

            modelBuilder.Entity<ReferenceDataItem>(entity =>
            {
                entity.ToTable("reference_data", "public");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.DataType).HasColumnName("data_type");
                entity.Property(e => e.Text).HasColumnName("text");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.CreatedOn).HasColumnName("created_on");
                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");
            });

            // Preserve existing relationship configurations
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
