using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Domain.Customers
{
    [Table("Customer", Schema = "dbo")]
    public class Customer : Entity
    {
        public string Sub { get; set; }

        public string? Username { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Phone { get; set; }
    }
}