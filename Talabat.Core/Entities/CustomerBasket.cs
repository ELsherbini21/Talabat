namespace Talabat.Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket(string id)
        {
            Id = id;
        }

        public string Id { get; set; } // Guid

        public List<BasketItems> Items { get; set; } = new List<BasketItems>(); // to make it Equal = zero .

    }
}
