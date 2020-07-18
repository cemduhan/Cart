using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.Models
{
    public class Carts
    {
        public long CartId { get; set; }
        public List<Item> CartItems { get; set; }
    }

    public class Item
    {
        public long ItemId { get; set; }
        public long Quantity { get; set; }
    }
}
