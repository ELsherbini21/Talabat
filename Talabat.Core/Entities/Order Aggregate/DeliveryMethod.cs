namespace Talabat.Core.Entities.Order_Aggregate
{
    public class DeliveryMethod : BaseEntity // related to my bussiness.
    {
        public DeliveryMethod()
        {

        }
        public DeliveryMethod(string shortName, string description, decimal cost, string deliveryTime)
        {
            ShortName = shortName;
            Description = description;
            Cost = cost;
            DeliveryTime = deliveryTime;
        }


        public string ShortName { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public string DeliveryTime { get; set; }

    }
}
