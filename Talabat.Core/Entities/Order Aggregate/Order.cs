using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate.Enum;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Order : BaseEntity
    {
        // Here Accessible Empty Parameter Less Constructor must be Exist . 
        public Order()
        {


        }
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> orderItems, decimal subtotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
        }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;// related to utc .

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress { get; set; }

        public int? DeliveryMethodId { get; set; } = null;//F.k for delivery Method . 

        public DeliveryMethod DeliveryMethod { get; set; } // M Order => 1 DeliveryMethod 

        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>(); // Many Navigation Property . 

        public decimal Subtotal { get; set; } // submission of [price * Quantity]

        // This is Derived Attribute By Convension  // mapped to Dervicd attribute . 
        public decimal GetTotal() => Subtotal + DeliveryMethod.Cost;

        public string PaymentIntentId { get; set; } = "";



    }

}
