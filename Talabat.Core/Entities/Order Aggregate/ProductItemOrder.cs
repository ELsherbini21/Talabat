namespace Talabat.Core.Entities.Order_Aggregate
{
    public class ProductItemOrder //Related To the Product , product will request as item . 
    {
        public ProductItemOrder()
        {

        }

        public ProductItemOrder(int productId, string productName, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string PictureUrl { get; set; }
    }
}
