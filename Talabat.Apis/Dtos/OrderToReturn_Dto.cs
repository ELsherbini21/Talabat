using Talabat.Core.Entities.Order_Aggregate.Enum;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Apis.Dtos
{
    public class OrderToReturn_Dto
    {
        public int Id { get; set; }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public string Status { get; set; }

        public Address ShippingAddress { get; set; }

        public string DeliveryMethod { get; set; }

        public decimal DeliveryMethodCost { get; set; }

        public ICollection<OrderItem_Dto> OrderItems { get; set; } = new HashSet<OrderItem_Dto>();

        public decimal Subtotal { get; set; }

        public decimal Total { get; set; } // By Convension He Will Read From GetTotal() Property || Method .

        public string PaymentIntentId { get; set; } = "";

    }
}
