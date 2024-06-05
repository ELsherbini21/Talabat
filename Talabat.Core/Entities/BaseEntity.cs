using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class BaseEntity // Base Class For Store Context . 
    {
        public int Id { get; set; }

    }

    public class CustomerBasket
    {
        public string Id { get; set; } // Guid

        public List<BasketItems> Items { get; set; }

    }

    // product that selected as basked inside item . 
    public class BasketItems
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string ProductPictureUrl { get; set; }

        public string CategoryName { get; set; }

        public string BrandName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
