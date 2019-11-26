using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<Item> Items { get; set; }

        public class Item
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }

        }
    }
}
