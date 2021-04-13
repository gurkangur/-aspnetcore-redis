using System.Collections.Generic;

namespace CartApi.Domain.Entities
{
    public class Cart
    {
        public string UserId { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
