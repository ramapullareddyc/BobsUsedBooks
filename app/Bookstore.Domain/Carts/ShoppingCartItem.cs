using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bookstore.Domain.Books;

namespace Bookstore.Domain.Carts
{
    [Table("ShoppingCartItem", Schema = "dbo")]
    public class ShoppingCartItem : Entity
    {
        // An empty constructor is required by EF Core
        private ShoppingCartItem() { }

        public ShoppingCartItem(ShoppingCart shoppingCart, int bookId, int quantity, bool wantToBuy)
        {
            ShoppingCartId = shoppingCart.Id;
            ShoppingCart = shoppingCart;
            BookId = bookId;
            Quantity = quantity;
            WantToBuy = wantToBuy;
        }

        [Column("ShoppingCartId")]
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        [Column("BookId")]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }

        [Column("WantToBuy")]
        public bool WantToBuy { get; set; }
    }
}