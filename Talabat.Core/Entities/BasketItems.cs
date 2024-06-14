namespace Talabat.Core.Entities
{
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
